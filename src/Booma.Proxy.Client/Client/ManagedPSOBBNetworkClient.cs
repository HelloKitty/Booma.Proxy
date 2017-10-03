using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using System.Net;
using System.Threading;
using Common.Logging;
using Common.Logging.Simple;

namespace Booma.Proxy
{
	/// <summary>
	/// High level client for consumption that manages the lower level networking.
	/// Provides a high level, typesafe API with simple interactions.
	/// </summary>
	public sealed class ManagedPSOBBNetworkClient<TClientType, TPayloadWriteType, TPayloadReadType> : IManagedNetworkClient<TPayloadWriteType, TPayloadReadType>, IDisposable
		where TPayloadWriteType : class
		where TPayloadReadType : class
		where TClientType : class, IDisconnectable, IConnectable, IPacketPayloadWritable<TPayloadWriteType>, IPacketPayloadReadable<TPayloadReadType>
	{
		//TODO: Syncronization maybe? Lots of issues could come up if connect or disconnect
		//is called at the same time.
		/// <inheritdoc />
		public bool isConnected { get; private set; }

		/// <summary>
		/// The outgoing message queue.
		/// </summary>
		private AsyncProducerConsumerQueue<TPayloadWriteType> OutgoingMessageQueue { get; }

		/// <summary>
		/// The incomding message queue.
		/// </summary>
		private AsyncProducerConsumerQueue<PSOBBNetworkIncomingMessage<TPayloadReadType>> IncomingMessageQueue { get; }

		//Not unmanaged in the C vs C# sense but unmanaged meaning that it doesn't really do anything
		//Unless interacted with or managed.
		/// <summary>
		/// The network client.
		/// </summary>
		private TClientType UnmanagedClient { get; }

		//TODO: Add indepth client logging.
		/// <summary>
		/// The client logger.
		/// </summary>
		private ILog Logger { get; }

		//TODO: Do we need to syncronize these?
		private List<CancellationTokenSource> TaskTokenSources { get; }

		/// <inheritdoc />
		public ManagedPSOBBNetworkClient([NotNull] TClientType unmanagedClient)
			: this(unmanagedClient, new NoOpLogger())
		{

		}

		public ManagedPSOBBNetworkClient([NotNull] TClientType unmanagedClient, [NotNull] ILog logger)
		{
			if(unmanagedClient == null) throw new ArgumentNullException(nameof(unmanagedClient));
			if(logger == null) throw new ArgumentNullException(nameof(logger));

			UnmanagedClient = unmanagedClient;
			Logger = logger;
			isConnected = false;
			TaskTokenSources = new List<CancellationTokenSource>(2);
			OutgoingMessageQueue = new AsyncProducerConsumerQueue<TPayloadWriteType>(); //TODO: Should we constrain max count?
			IncomingMessageQueue = new AsyncProducerConsumerQueue<PSOBBNetworkIncomingMessage<TPayloadReadType>>(); //TODO: Should we constrain max count?
		}

		/// <inheritdoc />
		public async Task<SendResult> SendMessage<TPayloadType>([NotNull] TPayloadType payload)
			where TPayloadType : class, TPayloadWriteType
		{
			if(payload == null) throw new ArgumentNullException(nameof(payload));

			//we just add the message to the queue and let the other threads/tasks deal with everything else.
			await OutgoingMessageQueue.EnqueueAsync(payload)
				.ConfigureAwait(false);

			return SendResult.Enqueued;
		}

		/// <inheritdoc />
		public async Task<PSOBBNetworkIncomingMessage<TPayloadReadType>> ReadMessageAsync()
		{
			return await IncomingMessageQueue.DequeueAsync()
				.ConfigureAwait(false);
		}

		private CancellationTokenSource CreateNewManagedCancellationTokenSource()
		{
			//Cretae and add to the token sources.
			CancellationTokenSource source = new CancellationTokenSource();
			TaskTokenSources.Add(source);
			return source;
		}

		/// <summary>
		/// Dispatches the outgoing messages scheduled to be send
		/// over the network.
		/// </summary>
		/// <returns>A future which will complete when the client disconnects.</returns>
		private async Task DispatchOutgoingMessages()
		{
			try
			{
				//We need a token for canceling this task when a user disconnects
				CancellationToken dispatchCancelation = CreateNewManagedCancellationTokenSource().Token;

				while(!dispatchCancelation.IsCancellationRequested)
					await UnmanagedClient.WriteAsync(await OutgoingMessageQueue.DequeueAsync(dispatchCancelation).ConfigureAwait(false))
						.ConfigureAwait(false);
			}
			catch(TaskCanceledException e)
			{
				//This is an expected exception that happens when the token is canceled
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Expected Task Canceled Exception: {e.Message}\n\n Stack: {e.StackTrace}");
				throw;
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Error: {e.Message}\n\n Stack: {e.StackTrace}");
				throw;
			}

			//TODO: Should we do anything after the dispatch has stopped?
		}

		/// <summary>
		/// Reading the incoming messages from the network client and schedules them
		/// with the incoming message queue.
		/// </summary>
		/// <returns>A future which will complete when the client disconnects.</returns>
		private async Task EnqueueIncomingMessages()
		{
			//We need a token for canceling this task when a user disconnects
			CancellationToken incomingCancellationToken = CreateNewManagedCancellationTokenSource().Token;

			try
			{
				while(!incomingCancellationToken.IsCancellationRequested)
				{
					PSOBBNetworkIncomingMessage<TPayloadReadType> message = await UnmanagedClient.ReadAsync(incomingCancellationToken)
						.ConfigureAwait(false);

					//if have to check the token again because the message may be null and may have been canceled mid-read
					if(incomingCancellationToken.IsCancellationRequested)
						continue;

					await IncomingMessageQueue.EnqueueAsync(message, incomingCancellationToken)
						.ConfigureAwait(false);
				}
			}
			catch(TaskCanceledException e)
			{
				//This is an expected exception that happens when the token is canceled
				if(Logger.IsDebugEnabled)
					Logger.Debug($"Expected Task Canceled Exception: {e.Message}\n\n Stack: {e.StackTrace}");
				throw;
			}
			catch(Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Error: {e.Message}\n\n Stack: {e.StackTrace}");
				throw;
			}

			//TODO: Should we do anything after the dispatch has stopped?
		}

		/// <inheritdoc />
		public async Task<bool> ConnectAsync(IPAddress address, int port)
		{
			//Disconnect if we're already connected
			if(isConnected)
				await DisconnectAsync(0)
					.ConfigureAwait(false);

			//This COULD return false, so we need to handle that
			isConnected = await UnmanagedClient.ConnectAsync(address, port)
				.ConfigureAwait(false);

			if(isConnected)
				StartNetworkIncomingOutgoingTasks();

			return isConnected;
		}

		private void StopAllNetworkTasks()
		{
			//Before disconnecting the managed client we should cancel all the tokens used for
			//running the tasks
			TaskTokenSources.ForEach(t =>
			{
				t.Cancel();

				//TODO: Is it safe not to dipose?
			});

			TaskTokenSources.Clear();
		}

		/// <inheritdoc />
		public void Disconnect() => DisconnectAsync(0).Wait();

		/// <inheritdoc />
		public async Task DisconnectAsync(int delay)
		{
			//Before disconnecting the managed client we should cancel all the tokens used for
			//running the tasks
			StopAllNetworkTasks();

			await UnmanagedClient.DisconnectAsync(delay)
				.ConfigureAwait(false);

			isConnected = false;
		}

		private void StartNetworkIncomingOutgoingTasks()
		{
			//Create both a read and write thread
			Task.Factory.StartNew(DispatchOutgoingMessages, TaskCreationOptions.LongRunning);
			Task.Factory.StartNew(EnqueueIncomingMessages, TaskCreationOptions.LongRunning);
		}

		/// <inheritdoc />
		public bool Connect(string ip, int port) => ConnectAsync(ip, port).Result;

		/// <inheritdoc />
		public bool Connect(IPAddress address, int port) => ConnectAsync(address, port).Result;

		/// <inheritdoc />
		public async Task<bool> ConnectAsync(string ip, int port) => await ConnectAsync(IPAddress.Parse(ip), port)
			.ConfigureAwait(false);

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		void Dispose(bool disposing)
		{
			if(!disposedValue)
			{
				if(disposing)
				{
					// TODO: dispose managed state (managed objects).
					StopAllNetworkTasks();
					//IncomingMessageQueue.Dispose();
					//OutgoingMessageQueue.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~ManagedPSOBBNetworkClient() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
