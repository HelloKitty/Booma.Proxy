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
	/// Marks the 0x6D command message with the associated operation code.
	/// Should be marked on <see cref="BaseSubCommand6D"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class SubCommand6DAttribute : WireDataContractBaseLinkAttribute
	{
		/// <inheritdoc />
		public SubCommand6DAttribute(SubCommand6DOperationCode opCode)
			: base((int)opCode, typeof(BaseSubCommand6D))
		{
			if(!Enum.IsDefined(typeof(SubCommand6DOperationCode), opCode)) throw new InvalidEnumArgumentException(nameof(opCode), (int)opCode, typeof(SubCommand6DOperationCode));
		}

		//Test ctor
		internal SubCommand6DAttribute(int opCode)
			: base(opCode, typeof(BaseSubCommand6D))
		{

		}
	}
}
