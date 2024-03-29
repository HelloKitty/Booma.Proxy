﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;

namespace Booma
{
	[AutoGeneratedWireMessageImplementationAttribute]
	public partial class SharedMarqueeScrollChangeEventPayload
	{
		public override Type SerializableType => typeof(SharedMarqueeScrollChangeEventPayload);
		public override PSOBBGamePacketPayloadServer Read(Span<byte> buffer, ref int offset)
		{
			SharedMarqueeScrollChangeEventPayload_Serializer.Instance.InternalRead(this, buffer, ref offset);
			return this;
		}

		public override void Write(PSOBBGamePacketPayloadServer value, Span<byte> buffer, ref int offset)
		{
			SharedMarqueeScrollChangeEventPayload_Serializer.Instance.InternalWrite(this, buffer, ref offset);
		}
	}
}

namespace FreecraftCore.Serializer
{
	[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
	//THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
	/// <summary>
	/// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
	/// code for the Type: <see cref = "SharedMarqueeScrollChangeEventPayload"/>
	/// </summary>
	public sealed partial class SharedMarqueeScrollChangeEventPayload_Serializer : BaseAutoGeneratedSerializerStrategy<SharedMarqueeScrollChangeEventPayload_Serializer, SharedMarqueeScrollChangeEventPayload>
	{
		/// <summary>
		/// Auto-generated deserialization/read method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalRead(SharedMarqueeScrollChangeEventPayload value, Span<byte> buffer, ref int offset)
		{
			//Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode
			;
			value.OperationCode = GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Read(buffer, ref offset);
			//Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[]
			;
			if (value.isFlagsSerialized)
			value.Flags = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Read(buffer, ref offset);
			//Type: SharedMarqueeScrollChangeEventPayload Field: 1 Name: unusued Type: Int64
			;
			value.unusued = GenericTypePrimitiveSerializerStrategy<Int64>.Instance.Read(buffer, ref offset);
			//Type: SharedMarqueeScrollChangeEventPayload Field: 2 Name: Message Type: String
			;
			value.Message = TerminatedStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, UTF16StringTerminatorTypeSerializerStrategy>.Instance.Read(buffer, ref offset);
		}

		/// <summary>
		/// Auto-generated serialization/write method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalWrite(SharedMarqueeScrollChangeEventPayload value, Span<byte> buffer, ref int offset)
		{
			//Type: PSOBBGamePacketPayloadServer Field: 1 Name: OperationCode Type: GameNetworkOperationCode
			;
			GenericPrimitiveEnumTypeSerializerStrategy<GameNetworkOperationCode, Int16>.Instance.Write(value.OperationCode, buffer, ref offset);
			//Type: PSOBBGamePacketPayloadServer Field: 2 Name: Flags Type: Byte[]
			;
			if (value.isFlagsSerialized)
			FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_4>.Instance.Write(value.Flags, buffer, ref offset);
			//Type: SharedMarqueeScrollChangeEventPayload Field: 1 Name: unusued Type: Int64
			;
			GenericTypePrimitiveSerializerStrategy<Int64>.Instance.Write(value.unusued, buffer, ref offset);
			//Type: SharedMarqueeScrollChangeEventPayload Field: 2 Name: Message Type: String
			;
			TerminatedStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, UTF16StringTerminatorTypeSerializerStrategy>.Instance.Write(value.Message, buffer, ref offset);
		}

		private sealed class StaticTypedNumeric_Int32_4 : StaticTypedNumeric<Int32>
		{
			public sealed override Int32 Value => 4;
		}
	}
}