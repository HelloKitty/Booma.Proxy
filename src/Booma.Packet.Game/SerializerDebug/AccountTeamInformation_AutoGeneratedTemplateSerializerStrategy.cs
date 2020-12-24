using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;

namespace FreecraftCore.Serializer
{
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="AccountTeamInformation"/>
    /// </summary>
    public sealed partial class AccountTeamInformation_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<AccountTeamInformation_AutoGeneratedTemplateSerializerStrategy, AccountTeamInformation>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(AccountTeamInformation value, Span<byte> buffer, ref int offset)
        {
            //Type: AccountTeamInformation Field: 1 Name: TeamId Type: UInt32;
            value.TeamId = GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Read(buffer, ref offset);
            //Type: AccountTeamInformation Field: 2 Name: TeamInformation Type: UInt32[];
            value.TeamInformation = FixedSizePrimitiveArrayTypeSerializerStrategy<uint, StaticTypedNumeric_Int32_2>.Instance.Read(buffer, ref offset);
            //Type: AccountTeamInformation Field: 3 Name: TeamPrivilege Type: UInt16;
            value.TeamPrivilege = GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Read(buffer, ref offset);
            //Type: AccountTeamInformation Field: 4 Name: reserved Type: UInt16;
            value.reserved = GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Read(buffer, ref offset);
            //Type: AccountTeamInformation Field: 5 Name: TeamName Type: String;
            value.TeamName = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Read(buffer, ref offset);
            //Type: AccountTeamInformation Field: 6 Name: TeamFlagByteRepresentation Type: Byte[];
            value.TeamFlagByteRepresentation = FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_2048>.Instance.Read(buffer, ref offset);
            //Type: AccountTeamInformation Field: 7 Name: TeamRewardsFlags Type: UInt32[];
            value.TeamRewardsFlags = FixedSizePrimitiveArrayTypeSerializerStrategy<uint, StaticTypedNumeric_Int32_2>.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(AccountTeamInformation value, Span<byte> buffer, ref int offset)
        {
            //Type: AccountTeamInformation Field: 1 Name: TeamId Type: UInt32;
            GenericTypePrimitiveSerializerStrategy<UInt32>.Instance.Write(value.TeamId, buffer, ref offset);
            //Type: AccountTeamInformation Field: 2 Name: TeamInformation Type: UInt32[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<uint, StaticTypedNumeric_Int32_2>.Instance.Write(value.TeamInformation, buffer, ref offset);
            //Type: AccountTeamInformation Field: 3 Name: TeamPrivilege Type: UInt16;
            GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Write(value.TeamPrivilege, buffer, ref offset);
            //Type: AccountTeamInformation Field: 4 Name: reserved Type: UInt16;
            GenericTypePrimitiveSerializerStrategy<UInt16>.Instance.Write(value.reserved, buffer, ref offset);
            //Type: AccountTeamInformation Field: 5 Name: TeamName Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Write(value.TeamName, buffer, ref offset);
            //Type: AccountTeamInformation Field: 6 Name: TeamFlagByteRepresentation Type: Byte[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<byte, StaticTypedNumeric_Int32_2048>.Instance.Write(value.TeamFlagByteRepresentation, buffer, ref offset);
            //Type: AccountTeamInformation Field: 7 Name: TeamRewardsFlags Type: UInt32[];
            FixedSizePrimitiveArrayTypeSerializerStrategy<uint, StaticTypedNumeric_Int32_2>.Instance.Write(value.TeamRewardsFlags, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_2 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 2; }
        private sealed class StaticTypedNumeric_Int32_16 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 16; }
        private sealed class StaticTypedNumeric_Int32_2048 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 2048; }
    }
}