using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Base class for all network clients.
	/// </summary>
	public abstract class NetworkClientBase : IConnectable, IDisconnectable, IDisposable,
		IBytesWrittable, IBytesReadable
	{
		/// <inheritdoc />
		public bool Connect(string ip, int port)
		{
			return ConnectAsync(IPAddress.Parse(ip), port).Result;
		}

		/// <inheritdoc />
		public bool Connect(IPAddress address, int port)
		{
			return ConnectAsync(address, port).Result;
		}

		/// <inheritdoc />
		public async Task<bool> ConnectAsync(string ip, int port)
		{
			return await ConnectAsync(IPAddress.Parse(ip), port);
		}

		/// <inheritdoc />
		public void Disconnect()
		{
			DisconnectAsync(0);
		}

		/// <inheritdoc />
		public void Write(byte[] bytes)
		{
			WriteAsync(bytes).Wait();
		}

		/// <inheritdoc />
		public byte[] Read(int count)
		{
			return ReadAsync(count, 0).Result;
		}

		/// <inheritdoc />
		public async Task<byte[]> ReadAsync(int count, int timeoutInMilliseconds)
		{
			byte[] buffer = new byte[count];

			return await ReadAsync(buffer, 0, count, timeoutInMilliseconds);
		}

		//Clients need only to implement the async subset methods
		//for the client to function. This will save a lot of duplication for potential
		//consumers of this base type

		/// <inheritdoc />
		public abstract Task<bool> ConnectAsync(IPAddress address, int port);

		/// <inheritdoc />
		public abstract Task DisconnectAsync(int delay);

		/// <inheritdoc />
		public abstract Task WriteAsync(byte[] bytes);

		/// <inheritdoc />
		public abstract Task<byte[]> ReadAsync(byte[] buffer, int start, int count, int timeoutInMilliseconds);

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if(!disposedValue)
			{
				if(disposing)
				{
				}


				disposedValue = true;
			}
		}

		// ~NetworkClientBase() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		void IDisposable.Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}
