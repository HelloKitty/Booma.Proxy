using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//Syl: https://github.com/Sylverant/ship_server/blob/master/src/subcmd.h#L412
	[WireDataContract]
	[SubCommand60Client(SubCommand60OperationCode.WrapToNewArea)]
	public sealed class Sub60WarpToNewAreaCommand : BaseSubCommand60Client
	{
		/// <summary>
		/// The ID associated with this client.
		/// </summary>
		[WireMember(1)]
		public short ClientId { get; }

		/// <summary>
		/// The Map/Level/Zone to warp to.
		/// (Ex. go from Forest 1 to Forest 2)
		/// </summary>
		[WireMember(2)]
		public int Zone { get; }

		/// <inheritdoc />
		public Sub60WarpToNewAreaCommand(short clientId, int zone)
		{
			if(zone < 0) throw new ArgumentOutOfRangeException(nameof(zone));
			if(clientId < 0) throw new ArgumentOutOfRangeException(nameof(clientId));

			ClientId = clientId;
			Zone = zone;
		}

		private Sub60WarpToNewAreaCommand()
		{
			
		}
	}
}
