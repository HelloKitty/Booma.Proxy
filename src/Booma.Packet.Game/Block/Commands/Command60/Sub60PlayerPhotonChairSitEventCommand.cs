using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma;
using FreecraftCore.Serializer;

namespace Booma
{
	/// <summary>
	/// Subcommand 60 packet sent when a player sits down in a photon chair.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.PhotonChairSit)]
	public sealed partial class Sub60PlayerPhotonChairSitEventCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		//TODO: Is this identifier?
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		/// <inheritdoc />
		public Sub60PlayerPhotonChairSitEventCommand(byte identifier)
			: this()
		{
			Identifier = identifier;
		}
		
		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60PlayerPhotonChairSitEventCommand()
			: base(SubCommand60OperationCode.PhotonChairSit)
		{

		}
	}
}
