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
	/// Marks the 0x60 command message with the associated operation code.
	/// Should be marked on <see cref="BaseSubCommand60"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class SubCommand60Attribute : WireDataContractBaseLinkAttribute
	{
		/// <inheritdoc />
		public SubCommand60Attribute(SubCommand60OperationCode opCode)
			: base((int)opCode, typeof(BaseSubCommand60))
		{
			if(!Enum.IsDefined(typeof(SubCommand60OperationCode), opCode)) throw new InvalidEnumArgumentException(nameof(opCode), (int)opCode, typeof(SubCommand60OperationCode));
		}

		//Test ctor
		internal SubCommand60Attribute(int opCode)
			: base(opCode, typeof(BaseSubCommand60))
		{
			
		}
	}
}
