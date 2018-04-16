using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma
{
	//Starts 16 bytes before the end of the stream.
	/// <summary>
	/// Model for the trailer at the end of the rel file.
	/// </summary>
	[WireDataContract]
	public sealed class NRelTrailerModel
	{
		/// <summary>
		/// The pointer/offset to the main block of data.
		/// </summary>
		[WireMember(1)]
		public uint MainBlockPointer { get; }

		//A bunch of unknown data after this.
		//See: https://docs.google.com/document/d/1B8bQsCJ9gU005INzePoK8fekj0O3Vootsq1FEpa8m6Y/edit#

		public NRelTrailerModel()
		{
			
		}
	}
}
