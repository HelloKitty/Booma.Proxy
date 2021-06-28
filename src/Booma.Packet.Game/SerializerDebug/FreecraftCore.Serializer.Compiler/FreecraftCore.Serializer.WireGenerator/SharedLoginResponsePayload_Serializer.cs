﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;

namespace Booma
{
	[AutoGeneratedWireMessageImplementationAttribute]
	public partial class SharedLoginResponsePayload
	{
		public override Type SerializableType => typeof(SharedLoginResponsePayload);
		public override PSOBBGamePacketPayloadServer Read(Span<byte> buffer, ref int offset)
		{
			SharedLoginResponsePayload_Serializer.Instance.InternalRead(this, buffer, ref offset);
			return this;
		}

		public override void Write(PSOBBGamePacketPayloadServer value, Span<byte> buffer, ref int offset)
		{
			SharedLoginResponsePayload_Serializer.Instance.InternalWrite(this, buffer, ref offset);
		}
	}
}

namespace FreecraftCore.Serializer
{
	[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
	//THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
	/// <summary>
	/// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
	/// code for the Type: <see cref = "SharedLoginResponsePayload"/>
	/// </summary>
	public sealed partial class SharedLoginResponsePayload_Serializer : BaseAutoGeneratedSerializerStrategy<SharedLoginResponsePayload_Serializer, SharedLoginResponsePayload>
	{
		/// <summary>
		/// Auto-generated deserialization/read method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalRead(SharedLoginResponsePayload value, Span<byte> buffer, ref int offset)
		{
			//Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode
			;
			value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Read(buffer, ref offset);
			//Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[]
			;
			if (value.isFlagsSerialized)
			value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 1 Name: ResponseCode Type: AuthenticationResponseCode
			;
			value.ResponseCode = GenericPrimitiveEnumTypeSerializerStrategy<AuthenticationResponseCode, Int32>.Instance.Read(buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 2 Name: Tag Type: Int32
			;
			value.Tag = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 3 Name: GuildCard Type: UInt32
			;
			value.GuildCard = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 4 Name: TeamId Type: Int32
			;
			value.TeamId = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 5 Name: SecurityData Type: SecurityData
			;
			value.SecurityData = SecurityData_Serializer.Instance.Read(buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 6 Name: Caps Type: Int32
			;
			value.Caps = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
		}

		/// <summary>
		/// Auto-generated serialization/write method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalWrite(SharedLoginResponsePayload value, Span<byte> buffer, ref int offset)
		{
			//Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode
			;
			GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Write(value.OperationCode, buffer, ref offset);
			//Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[]
			;
			if (value.isFlagsSerialized)
			FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 1 Name: ResponseCode Type: AuthenticationResponseCode
			;
			GenericPrimitiveEnumTypeSerializerStrategy<AuthenticationResponseCode, Int32>.Instance.Write(value.ResponseCode, buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 2 Name: Tag Type: Int32
			;
			GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.Tag, buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 3 Name: GuildCard Type: UInt32
			;
			GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.GuildCard, buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 4 Name: TeamId Type: Int32
			;
			GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.TeamId, buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 5 Name: SecurityData Type: SecurityData
			;
			SecurityData_Serializer.Instance.Write(value.SecurityData, buffer, ref offset);
			//Type: SharedLoginResponsePayload Field: 6 Name: Caps Type: Int32
			;
			GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.Caps, buffer, ref offset);
		}

		private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32>
		{
			public sealed override Int32 Value => 4;
		}
	}
}