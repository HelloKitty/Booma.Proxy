using System;
using System.Collections.Generic;
using System.Text;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	public sealed class CharacterJoinData
	{
		[WireMember(1)]
		public PlayerInformationHeader PlayerHeader { get; }

		[WireMember(2)]
		public CharacterInventoryData Inventory { get; }

		[WireMember(3)]
		public LobbyCharacterData Data { get; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private CharacterJoinData()
		{
			
		}
	}
}
