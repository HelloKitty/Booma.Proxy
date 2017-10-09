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
		public uint TeamId { get; }

		//TODO: What is this?
		[KnownSize(2)]
		[WireMember(2)]
		public uint[] TeamInformation { get; }

		//TODO: What is this? Can it be an enum?
		/// <summary>
		/// Privelege ranking/flags.
		/// </summary>
		[WireMember(3)]
		public ushort TeamPrivilege { get; }

		//TODO: What is this?
		[WireMember(4)]
		private ushort reserved { get; }

		/// <summary>
		/// The name of the team.
		/// </summary>
		[Encoding(EncodingType.UTF16)]
		[KnownSize(16)]
		[WireMember(5)]
		public string TeamName { get; }

		//TODO: Is this a known image format?
		/// <summary>
		/// The small image for the team (?)
		/// </summary>
		[KnownSize(2048)]
		[WireMember(6)]
		public byte[] TeamFlagByteRepresentation { get; }

		//TODO: Can this be an long enum flags?
		[KnownSize(2)]
		[WireMember(7)]
		public uint[] TeamRewardsFlags { get; }

		public AccountTeamInformation()
		{
			
		}
	}
}
