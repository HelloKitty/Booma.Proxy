using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Command sent by the server to set
	/// the game's experience rate.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.SetExperienceRate)]
	public sealed class Sub60SetExperienceRateCommand : BaseSubCommand60
	{
		/// <summary>
		/// The experience rate for the game/room.
		/// </summary>
		[WireMember(1)]
		public byte ExperienceRate { get; }

		//Serializer ctor
		private Sub60SetExperienceRateCommand()
		{
			
		}
	}
}
