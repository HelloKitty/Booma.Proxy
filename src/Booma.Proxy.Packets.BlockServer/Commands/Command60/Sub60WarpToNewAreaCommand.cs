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
	[SubCommand60(SubCommand60OperationCode.WrapToNewArea)]
	public sealed class Sub60WarpToNewAreaCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <summary>
		/// The ID associated with this client.
		/// </summary>
		[WireMember(1)]
		public byte Identifier { get; }

		//TODO: maybe leader?
		[WireMember(2)]
		private byte unusued { get; }

		/// <summary>
		/// The Map/Level/Zone to warp to.
		/// (Ex. go from Forest 1 to Forest 2)
		/// </summary>
		[WireMember(3)]
		public int Zone { get; }

		/// <inheritdoc />
		public Sub60WarpToNewAreaCommand(byte clientId, int zone)
			: this()
		{
			if(zone < 0) throw new ArgumentOutOfRangeException(nameof(zone));

			Identifier = clientId;
			Zone = zone;
		}

		private Sub60WarpToNewAreaCommand()
		{
			//Calc static 32bit size
			CommandSize = 8 / 4;
		}
	}
}
