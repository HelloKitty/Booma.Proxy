using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Booma.Proxy;
using FreecraftCore.Serializer;
using GladNet;
using JetBrains.Annotations;

namespace FreecraftCore
{
	public sealed class PsobbNetworkSerializers : NetworkSerializerServicePair
	{
		private static INetworkSerializationService Serializer { get; } = BuildClientSerializer();

		/// <inheritdoc />
		public PsobbNetworkSerializers()
			: base(Serializer, Serializer)
		{

		}

		//TODO: We should use seperate assemblies that can build the desired serializers
		private static INetworkSerializationService BuildServerSerializer()
		{
			return BuildClientSerializer();
		}

		private static INetworkSerializationService BuildClientSerializer()
		{
			SerializerService serializer = new SerializerService();

			foreach(Type t in PacketGameStubMetadataMarker.SerializableTypes)
					serializer.RegisterType(t);

			//Also register the welcome, since it is critical to setting up the connection
			//Also we need redirection so we can redirect the connections
			serializer.RegisterType<SharedWelcomePayload>();
			serializer.RegisterType<SharedConnectionRedirectPayload>();

			//Also the header types
			serializer.RegisterType<PSOBBPacketHeader>();

			serializer.Compile();

			return new FreecraftCoreGladNetSerializerAdapter(serializer);
		}
	}
}
