﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;

namespace Booma
{
	[AutoGeneratedWireMessageImplementationAttribute]
	public partial class Sub60ClientBoxHitEventCommand
	{
		public override Type SerializableType => typeof(Sub60ClientBoxHitEventCommand);
		public override BaseSubCommand60 Read(Span<byte> buffer, ref int offset)
		{
			Sub60ClientBoxHitEventCommand_Serializer.Instance.InternalRead(this, buffer, ref offset);
			return this;
		}

		public override void Write(BaseSubCommand60 value, Span<byte> buffer, ref int offset)
		{
			Sub60ClientBoxHitEventCommand_Serializer.Instance.InternalWrite(this, buffer, ref offset);
		}
	}
}

namespace FreecraftCore.Serializer
{
	[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
	//THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
	/// <summary>
	/// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
	/// code for the Type: <see cref = "Sub60ClientBoxHitEventCommand"/>
	/// </summary>
	public sealed partial class Sub60ClientBoxHitEventCommand_Serializer : BaseAutoGeneratedSerializerStrategy<Sub60ClientBoxHitEventCommand_Serializer, Sub60ClientBoxHitEventCommand>
	{
		/// <summary>
		/// Auto-generated deserialization/read method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalRead(Sub60ClientBoxHitEventCommand value, Span<byte> buffer, ref int offset)
		{
			//Type: BaseSubCommand60 Field: 1 Name: CommandOperationCode Type: SubCommand60OperationCode
			;
			value.CommandOperationCode = GenericPrimitiveEnumTypeSerializerStrategy<SubCommand60OperationCode, Byte>.Instance.Read(buffer, ref offset);
			//Type: BaseSubCommand60 Field: 2 Name: CommandSize Type: Byte
			;
			if (value.isSizeSerialized)
			value.CommandSize = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
			//Type: Sub60ClientBoxHitEventCommand Field: 1 Name: ObjectIdentifier Type: MapObjectIdentifier
			;
			value.ObjectIdentifier = MapObjectIdentifier_Serializer.Instance.Read(buffer, ref offset);
			//Type: Sub60ClientBoxHitEventCommand Field: 2 Name: unk1 Type: Int32
			;
			value.unk1 = GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Read(buffer, ref offset);
			//Type: Sub60ClientBoxHitEventCommand Field: 3 Name: Identifier2_unk2 Type: Int16
			;
			value.Identifier2_unk2 = GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Read(buffer, ref offset);
			//Type: Sub60ClientBoxHitEventCommand Field: 4 Name: unk3 Type: Int16
			;
			value.unk3 = GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Read(buffer, ref offset);
		}

		/// <summary>
		/// Auto-generated serialization/write method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalWrite(Sub60ClientBoxHitEventCommand value, Span<byte> buffer, ref int offset)
		{
			//Type: BaseSubCommand60 Field: 1 Name: CommandOperationCode Type: SubCommand60OperationCode
			;
			GenericPrimitiveEnumTypeSerializerStrategy<SubCommand60OperationCode, Byte>.Instance.Write(value.CommandOperationCode, buffer, ref offset);
			//Type: BaseSubCommand60 Field: 2 Name: CommandSize Type: Byte
			;
			if (value.isSizeSerialized)
			BytePrimitiveSerializerStrategy.Instance.Write(value.CommandSize, buffer, ref offset);
			//Type: Sub60ClientBoxHitEventCommand Field: 1 Name: ObjectIdentifier Type: MapObjectIdentifier
			;
			MapObjectIdentifier_Serializer.Instance.Write(value.ObjectIdentifier, buffer, ref offset);
			//Type: Sub60ClientBoxHitEventCommand Field: 2 Name: unk1 Type: Int32
			;
			GenericTypePrimitiveSerializerStrategy<Int32>.Instance.Write(value.unk1, buffer, ref offset);
			//Type: Sub60ClientBoxHitEventCommand Field: 3 Name: Identifier2_unk2 Type: Int16
			;
			GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Write(value.Identifier2_unk2, buffer, ref offset);
			//Type: Sub60ClientBoxHitEventCommand Field: 4 Name: unk3 Type: Int16
			;
			GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Write(value.unk3, buffer, ref offset);
		}
	}
}