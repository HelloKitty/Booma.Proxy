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
	/// Marks the 0x60 command client payload with the associated operation code.
	/// Should be marked on <see cref="BaseSubCommand60Client"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class SubCommand60ClientAttribute : WireDataContractBaseLinkAttribute
	{
		/// <inheritdoc />
		public SubCommand60ClientAttribute(SubCommand60OperationCode opCode)
			: base((int)opCode, typeof(BaseSubCommand60Client))
		{
			if(!Enum.IsDefined(typeof(SubCommand60OperationCode), opCode)) throw new InvalidEnumArgumentException(nameof(opCode), (int)opCode, typeof(SubCommand60OperationCode));
		}
	}
}
