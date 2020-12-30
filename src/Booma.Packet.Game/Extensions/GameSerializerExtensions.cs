using System;
using System.Collections.Generic;
using System.Text;
using Booma.Proxy;
using FreecraftCore.Serializer;

namespace Booma
{
	public static class GameSerializerExtensions
	{
		/// <summary>
		/// Registers packet serializers for <see cref="PSOBBGamePacketPayloadClient"/> and <see cref="PSOBBGamePacketPayloadServer"/>.
		/// </summary>
		/// <typeparam name="TSerializerType">The serializer type.</typeparam>
		/// <param name="serializer">The serializer.</param>
		/// <returns>The serializer for method chaining.</returns>
		public static TSerializerType RegisterGamePacketSerializers<TSerializerType>(this TSerializerType serializer)
			where TSerializerType : ISerializationPolymorphicRegister
		{
			serializer.RegisterPolymorphicSerializer<PSOBBGamePacketPayloadClient, PSOBBGamePacketPayloadClient_Serializer>();
			serializer.RegisterPolymorphicSerializer<PSOBBGamePacketPayloadServer, PSOBBGamePacketPayloadServer_Serializer>();

			serializer.RegisterPolymorphicSerializer<BaseSubCommand60, BaseSubCommand60_Serializer>();
			serializer.RegisterPolymorphicSerializer<BaseSubCommand62, BaseSubCommand62_Serializer>();
			serializer.RegisterPolymorphicSerializer<BaseSubCommand6D, BaseSubCommand6D_Serializer>();

			return serializer;
		}
	}
}
