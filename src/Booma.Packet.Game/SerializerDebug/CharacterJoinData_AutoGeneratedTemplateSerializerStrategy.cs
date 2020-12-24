using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FreecraftCore.Serializer;
using Booma.Proxy;

namespace FreecraftCore.Serializer
{
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    //THIS CODE IS FOR AUTO-GENERATED SERIALIZERS! DO NOT MODIFY UNLESS YOU KNOW WELL!
    /// <summary>
    /// FreecraftCore.Serializer's AUTO-GENERATED (do not edit) serialization
    /// code for the Type: <see cref="CharacterJoinData"/>
    /// </summary>
    public sealed partial class CharacterJoinData_AutoGeneratedTemplateSerializerStrategy
            : BaseAutoGeneratedSerializerStrategy<CharacterJoinData_AutoGeneratedTemplateSerializerStrategy, CharacterJoinData>
    {
        /// <summary>
        /// Auto-generated deserialization/read method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalRead(CharacterJoinData value, Span<byte> buffer, ref int offset)
        {
            //Type: CharacterJoinData Field: 1 Name: PlayerHeader Type: PlayerInformationHeader;
            value.PlayerHeader = PlayerInformationHeader_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: CharacterJoinData Field: 2 Name: Inventory Type: CharacterInventoryData;
            value.Inventory = CharacterInventoryData_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
            //Type: CharacterJoinData Field: 3 Name: Data Type: LobbyCharacterData;
            value.Data = LobbyCharacterData_AutoGeneratedTemplateSerializerStrategy.Instance.Read(buffer, ref offset);
        }

        /// <summary>
        /// Auto-generated serialization/write method.
        /// Partial method implemented from shared partial definition.
        /// </summary>
        /// <param name="value">See external doc.</param>
        /// <param name="buffer">See external doc.</param>
        /// <param name="offset">See external doc.</param>
        public override void InternalWrite(CharacterJoinData value, Span<byte> buffer, ref int offset)
        {
            //Type: CharacterJoinData Field: 1 Name: PlayerHeader Type: PlayerInformationHeader;
            PlayerInformationHeader_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.PlayerHeader, buffer, ref offset);
            //Type: CharacterJoinData Field: 2 Name: Inventory Type: CharacterInventoryData;
            CharacterInventoryData_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Inventory, buffer, ref offset);
            //Type: CharacterJoinData Field: 3 Name: Data Type: LobbyCharacterData;
            LobbyCharacterData_AutoGeneratedTemplateSerializerStrategy.Instance.Write(value.Data, buffer, ref offset);
        }
    }
}