using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//Syl: https://github.com/Sylverant/ship_server/blob/master/src/subcmd.h#L412
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.WrapToNewArea)]
	public sealed partial class Sub60WarpToNewAreaCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <summary>
		/// The ID associated with this client.
		/// </summary>
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		//TODO: maybe leader?
		[WireMember(2)]
		internal byte unusued { get; set; }

		/// <summary>
		/// The Map/Level/Zone to warp to.
		/// (Ex. go from Forest 1 to Forest 2)
		/// </summary>
		[WireMember(3)]
		public int Zone { get; internal set; }

		/// <inheritdoc />
		public Sub60WarpToNewAreaCommand(byte clientId, int zone)
			: this()
		{
			if(zone < 0) throw new ArgumentOutOfRangeException(nameof(zone));

			Identifier = clientId;
			Zone = zone;
		}

		/// <inheritdoc />
		public Sub60WarpToNewAreaCommand(int clientId, int zone)
			: this((byte)clientId, zone)
		{

		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60WarpToNewAreaCommand()
			: base(SubCommand60OperationCode.WrapToNewArea)
		{
			//Calc static 32bit size
			CommandSize = 8 / 4;
		}
	}
}
