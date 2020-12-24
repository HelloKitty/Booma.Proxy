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
    /// code for the Type: <see cref="GameListEntry"/>
    /// </summary>
    public sealed partial class GameListEntry_AutoGeneratedTemplateSerializerStrategy
        : BaseAutoGeneratedSerializerStrategy<GameListEntry_AutoGeneratedTemplateSerializerStrategy, GameListEntry>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(GameListEntry value, Span<byte> buffer, ref int offset)
        {
            //Type: GameListEntry Field: 1 Name: Listing Type: MenuItemIdentifier;
            value.Listing = MenuItemIdentifier_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: GameListEntry Field: 2 Name: Difficulty Type: DifficultyType;
            value.Difficulty = GenericPrimitiveEnumTypeSerializerStrategy<DifficultyType, Byte>.Instance.Read(buffer, ref offset);
            //Type: GameListEntry Field: 3 Name: PlayerCount Type: Byte;
            value.PlayerCount = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: GameListEntry Field: 4 Name: Name Type: String;
            value.Name = FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Read(buffer, ref offset);
            //Type: GameListEntry Field: 5 Name: Episode Type: EpisodeType;
            value.Episode = GenericPrimitiveEnumTypeSerializerStrategy<EpisodeType, Byte>.Instance.Read(buffer, ref offset);
            //Type: GameListEntry Field: 6 Name: flags Type: Byte;
            value.flags = BytePrimitiveSerializerStrategy.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(GameListEntry value, Span<byte> buffer, ref int offset)
        {
            //Type: GameListEntry Field: 1 Name: Listing Type: MenuItemIdentifier;
            MenuItemIdentifier_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Listing, buffer, ref offset);
            //Type: GameListEntry Field: 2 Name: Difficulty Type: DifficultyType;
            GenericPrimitiveEnumTypeSerializerStrategy<DifficultyType, Byte>.Instance.Write(value.Difficulty, buffer, ref offset);
            //Type: GameListEntry Field: 3 Name: PlayerCount Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.PlayerCount, buffer, ref offset);
            //Type: GameListEntry Field: 4 Name: Name Type: String;
            FixedSizeStringTypeSerializerStrategy<UTF16StringTypeSerializerStrategy, StaticTypedNumeric_Int32_16>.Instance.Write(value.Name, buffer, ref offset);
            //Type: GameListEntry Field: 5 Name: Episode Type: EpisodeType;
            GenericPrimitiveEnumTypeSerializerStrategy<EpisodeType, Byte>.Instance.Write(value.Episode, buffer, ref offset);
            //Type: GameListEntry Field: 6 Name: flags Type: Byte;
            BytePrimitiveSerializerStrategy.Instance.Write(value.flags, buffer, ref offset);
        }
        private sealed class StaticTypedNumeric_Int32_16 : StaticTypedNumeric<Int32> { public sealed override Int32 Value => 16; }
    }
}