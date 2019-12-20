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
		[WireMember(1)]
		private short tagChunk1 { get; }

		/// <summary>
		/// Indicates if the player header is filled with player
		/// information. It may be uninitialized or empty.
		/// </summary>
		[WireMember(2)]
		public bool isSlotFilled { get; }

		[WireMember(3)]
		private byte tagChunk2 { get; }

		/// <summary>
		/// The guild card number of the player.
		/// </summary>
		[WireMember(4)]
		public uint GuildCardNumber { get; }

		//TODO: What is this?
		[KnownSize(3)] //Syl says there are 3 of these but Soda's Teth skips x10 from the begining of GCN meaning he considers there is only 4
		[WireMember(5)]
		private uint[] unk1 { get; }

		/// <summary>
		/// The ID of the client.
		/// </summary>
		[WireMember(6)] //TODO: Should this be a byte?
		public int ClientId { get; }

		//TODO: What is this?
		[KnownSize(2)]
		[WireMember(7)]
		private int[] unk2 { get; }

		[Encoding(EncodingType.UTF16)]
		[KnownSize(16)]
		[WireMember(8)]
		public string CharacterName { get; }

		//TODO: What is this?
		[WireMember(9)]
		private uint unk3 { get; }

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