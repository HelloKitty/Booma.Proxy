using System;
using System.Collections.Generic;
using System.Text;
using Booma;
using FreecraftCore.Serializer;

namespace Booma
{
	public static class PatchSerializerExtensions
	{
		/// <summary>
		/// Registers packet serializers for <see cref="PSOBBPatchPacketPayloadServer"/> and <see cref="PSOBBPatchPacketPayloadClient"/>.
		/// </summary>
		/// <typeparam name="TSerializerType">The serializer type.</typeparam>
		/// <param name="serializer">The serializer.</param>
		/// <returns>The serializer for method chaining.</returns>
		public static TSerializerType RegisterPatchPacketSerializers<TSerializerType>(this TSerializerType serializer)
			where TSerializerType : ISerializationPolymorphicRegister
		{
			serializer.RegisterPolymorphicSerializer<PSOBBPatchPacketPayloadClient, PSOBBPatchPacketPayloadClient_Serializer>();
			serializer.RegisterPolymorphicSerializer<PSOBBPatchPacketPayloadServer, PSOBBPatchPacketPayloadServer_Serializer>();

			return serializer;
		}
	}
}
