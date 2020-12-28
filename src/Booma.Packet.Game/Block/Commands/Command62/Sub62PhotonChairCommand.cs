using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[SubCommand62(SubCommand62OperationCode.PhotonChairCommand)]
	public sealed partial class Sub62PhotonChairCommand : BaseSubCommand62
	{
		/// <summary>
		/// TODO What is this?
		/// </summary>
		[KnownSize(14)]
		[WireMember(2)]
		public byte[] UnknownBytes { get; internal set; } = Array.Empty<byte>();

		public Sub62PhotonChairCommand()
			: base(SubCommand62OperationCode.PhotonChairCommand)
		{
			//Calc static 32bit size
			CommandSize = 16 / 4;
		}
	}
}
