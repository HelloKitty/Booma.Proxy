﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma;

namespace Booma
{
	[AutoGeneratedWireMessageImplementationAttribute]
	public partial class Sub60PlayerLeftGameEventCommand
	{
		public override Type SerializableType => typeof(Sub60PlayerLeftGameEventCommand);
		public override BaseSubCommand60 Read(Span<byte> buffer, ref int offset)
		{
			Sub60PlayerLeftGameEventCommand_Serializer.Instance.InternalRead(this, buffer, ref offset);
			return this;
		}

		public override void Write(BaseSubCommand60 value, Span<byte> buffer, ref int offset)
		{
			Sub60PlayerLeftGameEventCommand_Serializer.Instance.InternalWrite(this, buffer, ref offset);
		}
	}
}

namespace FreecraftCore.Serializer
{
	[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
	//THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
	/// <summary>
	/// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
	/// code for the Type: <see cref = "Sub60PlayerLeftGameEventCommand"/>
	/// </summary>
	public sealed partial class Sub60PlayerLeftGameEventCommand_Serializer : BaseAutoGeneratedSerializerStrategy<Sub60PlayerLeftGameEventCommand_Serializer, Sub60PlayerLeftGameEventCommand>
	{
		/// <summary>
		/// Auto-generated deserialization/read method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalRead(Sub60PlayerLeftGameEventCommand value, Span<byte> buffer, ref int offset)
		{
			//Type: BaseSubCommand60 Field: 1 Name: CommandOperationCode Type: SubCommand60OperationCode
			;
			value.CommandOperationCode = GenericPrimitiveEnumTypeSerializerStrategy<SubCommand60OperationCode, Byte>.Instance.Read(buffer, ref offset);
			//Type: BaseSubCommand60 Field: 2 Name: CommandSize Type: Byte
			;
			if (value.isSizeSerialized)
			value.CommandSize = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
			//Type: Sub60PlayerLeftGameEventCommand Field: 1 Name: Identifier Type: Byte
			;
			value.Identifier = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
			//Type: Sub60PlayerLeftGameEventCommand Field: 2 Name: LeaderId Type: Byte
			;
			value.LeaderId = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
			//Type: Sub60PlayerLeftGameEventCommand Field: 3 Name: unk Type: Int16
			;
			value.unk = GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Read(buffer, ref offset);
		}

		/// <summary>
		/// Auto-generated serialization/write method.
		/// Partial method implemented from shared partial definition.
		/// </summary>
		/// <param name = "value">See external doc.</param>
		/// <param name = "buffer">See external doc.</param>
		/// <param name = "offset">See external doc.</param>
		public override void InternalWrite(Sub60PlayerLeftGameEventCommand value, Span<byte> buffer, ref int offset)
		{
			//Type: BaseSubCommand60 Field: 1 Name: CommandOperationCode Type: SubCommand60OperationCode
			;
			GenericPrimitiveEnumTypeSerializerStrategy<SubCommand60OperationCode, Byte>.Instance.Write(value.CommandOperationCode, buffer, ref offset);
			//Type: BaseSubCommand60 Field: 2 Name: CommandSize Type: Byte
			;
			if (value.isSizeSerialized)
			BytePrimitiveSerializerStrategy.Instance.Write(value.CommandSize, buffer, ref offset);
			//Type: Sub60PlayerLeftGameEventCommand Field: 1 Name: Identifier Type: Byte
			;
			BytePrimitiveSerializerStrategy.Instance.Write(value.Identifier, buffer, ref offset);
			//Type: Sub60PlayerLeftGameEventCommand Field: 2 Name: LeaderId Type: Byte
			;
			BytePrimitiveSerializerStrategy.Instance.Write(value.LeaderId, buffer, ref offset);
			//Type: Sub60PlayerLeftGameEventCommand Field: 3 Name: unk Type: Int16
			;
			GenericTypePrimitiveSerializerStrategy<Int16>.Instance.Write(value.unk, buffer, ref offset);
		}
	}
}