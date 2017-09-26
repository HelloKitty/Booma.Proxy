using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Metadata attributes that implement <see cref="WireDataContractBaseLinkAttribute"/> making
	/// it easier to safely link child payloads to their propert base type and
	/// associate with in a typesafe fashion with their network operationcode enumeration value.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class LoginServerPacketPayloadAttribute : WireDataContractBaseLinkAttribute
	{
		public LoginServerPacketPayloadAttribute(int index) 
			: base(index, typeof(PSOBBLoginPacketPayloadServer))
		{

		}
	}
}
