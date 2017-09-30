using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public static class HandlerRegisterationExtensions
	{
		/// <summary>
		/// Registers a handler with the dependency container builder.
		/// </summary>
		/// <typeparam name="THandlerType">The handler type to register.</typeparam>
		/// <param name="builder">The builder.</param>
		public static void RegisterHandler<THandlerType, TPayloadType>([NotNull] this ContainerBuilder builder)
			where THandlerType : IClientPayloadSpecificMessageHandler<TPayloadType, PSOBBPatchPacketPayloadClient>
			where TPayloadType : PSOBBPatchPacketPayloadServer
		{
			if(builder == null) throw new ArgumentNullException(nameof(builder));

			//Patch login ready
			builder.RegisterType<THandlerType>()
				.SingleInstance();

			builder.Register(i => i.Resolve<THandlerType>().AsTryHandler())
				.As<IClientMessageHandler<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient>>();
		}

		/// <summary>
		/// Registers a handler with the dependency container builder.
		/// </summary>
		/// <typeparam name="THandlerType">The handler type to register.</typeparam>
		/// <param name="builder">The builder.</param>
		public static void RegisterHandler<THandlerType, TPayloadType>([NotNull] this ContainerBuilder builder, Action<THandlerType> onResolve)
			where THandlerType : IClientPayloadSpecificMessageHandler<TPayloadType, PSOBBPatchPacketPayloadClient>
			where TPayloadType : PSOBBPatchPacketPayloadServer
		{
			if(builder == null) throw new ArgumentNullException(nameof(builder));

			//Patch login ready
			builder.RegisterType<THandlerType>()
				.SingleInstance();

			builder.Register(i =>
				{
					THandlerType resolve = i.Resolve<THandlerType>();

					onResolve(resolve);

					return resolve.AsTryHandler();
				})
				.As<IClientMessageHandler<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadClient>>();
		}
	}
}
