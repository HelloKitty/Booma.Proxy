using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	public sealed class CharacterJoinData
	{
		[WireMember(1)]
		public PlayerInformationHeader PlayerHeader { get; internal set; }

		[WireMember(2)]
		public CharacterInventoryData Inventory { get; internal set; }

		[WireMember(3)]
		public LobbyCharacterData Data { get; internal set; }

		public CharacterJoinData([NotNull] PlayerInformationHeader playerHeader, [NotNull] CharacterInventoryData inventory, [NotNull] LobbyCharacterData data)
			: this()
		{
			PlayerHeader = playerHeader ?? throw new ArgumentNullException(nameof(playerHeader));
			Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
			Data = data ?? throw new ArgumentNullException(nameof(data));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterJoinData()
		{
			
		}
	}
}
