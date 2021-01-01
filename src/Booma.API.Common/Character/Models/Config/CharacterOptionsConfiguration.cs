using System;
using System.Collections.Generic;
using System.Text;
using Booma.Proxy;
using FreecraftCore.Serializer;

namespace Booma
{
	[WireDataContract]
	public sealed class CharacterOptionsConfiguration
	{
		/// <summary>
		/// Unknown bytes.
		/// </summary>
		[KnownSize(276)]
		[WireMember(1)]
		internal byte[] unk { get; set; } = Array.Empty<byte>();

		/// <summary>
		/// Binding configuration.
		/// </summary>
		[WireMember(2)]
		public BindingsConfig Bindings { get; internal set; }

		/// <summary>
		/// The guild card for the account.
		/// </summary>
		[WireMember(3)]
		public uint GuildCard { get; internal set; }

		/// <summary>
		/// The team information.
		/// </summary>
		[WireMember(4)]
		public AccountTeamInformation TeamInfo { get; internal set; }

		public CharacterOptionsConfiguration(BindingsConfig bindings, uint guildCard, AccountTeamInformation teamInfo)
		{
			Bindings = bindings;
			GuildCard = guildCard;
			TeamInfo = teamInfo;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public CharacterOptionsConfiguration()
		{
			
		}
	}
}
