using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Information/config option containing info about the team data.
	/// </summary>
	[WireDataContract]
	public sealed class AccountTeamInformation
	{
		/// <summary>
		/// ID for the team.
		/// </summary>
		[WireMember(1)]
		public uint TeamId { get; internal set; }

		//TODO: What is this?
		[KnownSize(2)]
		[WireMember(2)]
		public uint[] TeamInformation { get; internal set; }

		//TODO: What is this? Can it be an enum?
		/// <summary>
		/// Privelege ranking/flags.
		/// </summary>
		[WireMember(3)]
		public ushort TeamPrivilege { get; internal set; }

		//TODO: What is this?
		[WireMember(4)]
		internal ushort reserved { get; set; }

		/// <summary>
		/// The name of the team.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[DontTerminate]
		[KnownSize(16)]
		[WireMember(5)]
		public string TeamName { get; internal set; }

		//TODO: Is this a known image format?
		/// <summary>
		/// The small image for the team (?)
		/// </summary>
		[KnownSize(2048)]
		[WireMember(6)]
		public byte[] TeamFlagByteRepresentation { get; internal set; } = Array.Empty<byte>();

		[WireMember(7)]
		public ulong TeamRewardsFlags { get; internal set; }

		public AccountTeamInformation(uint teamId, uint[] teamInformation, ushort teamPrivilege, ushort reserved, string teamName, ulong teamRewardsFlags)
		{
			TeamId = teamId;
			TeamInformation = teamInformation;
			TeamPrivilege = teamPrivilege;
			this.reserved = reserved;
			TeamName = teamName;
			TeamRewardsFlags = teamRewardsFlags;
		}

		public AccountTeamInformation()
		{
			
		}
	}
}
