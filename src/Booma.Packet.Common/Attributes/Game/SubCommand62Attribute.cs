using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Marks the 0x62 command message with the associated operation code.
	/// Should be marked on <see cref="BaseSubCommand62"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class SubCommand62Attribute : WireDataContractBaseLinkAttribute
	{
		/// <inheritdoc />
		public SubCommand62Attribute(SubCommand62OperationCode opCode)
			: base((int)opCode, typeof(BaseSubCommand62))
		{
			if(!Enum.IsDefined(typeof(SubCommand62OperationCode), opCode)) throw new InvalidEnumArgumentException(nameof(opCode), (int)opCode, typeof(SubCommand60OperationCode));
		}

		//Test ctor
		internal SubCommand62Attribute(int opCode)
			: base(opCode, typeof(BaseSubCommand62))
		{

		}
	}
}
