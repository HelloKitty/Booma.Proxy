using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Header for player information.
	/// </summary>
	[WireDataContract]
	public sealed class PlayerInformationHeader
	{
		//TODO: What is this?
		[WireMember(1)]
		private uint Tag { get; }

		/// <summary>
		/// The guild card number of the player.
		/// </summary>
		[WireMember(2)]
		public uint GuildCardNumber { get; }

		//TODO: What is this?
		[KnownSize(5)]
		[WireMember(3)]
		private uint[] unk1 { get; }

		/// <summary>
		/// The ID of the client.
		/// </summary>
		[WireMember(4)]
		public int ClientId { get; }

		[KnownSize(16)]
		[WireMember(5)]
		public string CharacterName { get; }

		[WireMember(6)]
		private uint unk2 { get; }

		//Serializer ctor
		private PlayerInformationHeader()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"Id: {ClientId} Name: {CharacterName} GCN: {GuildCardNumber} Tag: {Tag}";
		}
	}
}