using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//Based on: https://github.com/Sylverant/ship_server/blob/9373df882859b234bc3e299d2e85f7b4c515d025/src/player.h#L54
	/// <summary>
	/// Header for player information.
	/// </summary>
	[WireDataContract]
	public sealed class PlayerInformationHeader
	{
		//TODO: What is this?
		/*[WireMember(1)]
		internal short tagChunk1 { get; set; }

		/// <summary>
		/// Indicates if the player header is filled with player
		/// information. It may be uninitialized or empty.
		/// </summary>
		[WireMember(2)]
		public bool isSlotFilled { get; internal set; }

		[WireMember(3)]
		internal byte tagChunk2 { get; set; }*/

		//TODO: Remove.
		//[WireMember(2)]
		public bool isSlotFilled { get; internal set; }

		//Sylverant doesn't have this, Teth does and Soly uses it too.
		[WireMember(4)]
		internal int unk4 { get; set; }

		/// <summary>
		/// The guild card number of the player.
		/// </summary>
		[WireMember(5)]
		public uint GuildCardNumber { get; internal set; }

		//TODO: What is this?
		[KnownSize(5)] //Syl says there are 3 of these but Soda's Teth skips x10 from the begining of GCN meaning he considers there is only 4
		[WireMember(6)]
		internal uint[] unk1 { get; set; }

		/// <summary>
		/// The ID of the client.
		/// </summary>
		[WireMember(7)] //TODO: Should this be a byte?
		public int ClientId { get; internal set; }

		[Encoding(EncodingType.UTF16)]
		[KnownSize(16)]
		[WireMember(9)]
		public string CharacterName { get; internal set; }

		//TODO: What is this?
		[WireMember(10)]
		internal uint unk3 { get; set; }

		//Serializer ctor
		private PlayerInformationHeader()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if(isSlotFilled)
				return $"isInitialized: {isSlotFilled} Id: {ClientId} Name: {CharacterName} GCN: {GuildCardNumber}";
			else
				return $"isInitialized: {isSlotFilled}";
		}
	}
}
