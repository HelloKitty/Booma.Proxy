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
		public PlayerInformationHeader PlayerHeader { get; internal set; }

		[WireMember(2)]
		public CharacterInventoryData Inventory { get; internal set; }

		[WireMember(3)]
		public LobbyCharacterData Data { get; internal set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private CharacterJoinData()
		{
			
		}
	}
}
