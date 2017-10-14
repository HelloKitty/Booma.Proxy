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
	/// Marks the 0x60 command server payload with the associated operation code.
	/// Should be marked on <see cref="BlockNetworkCommandEventServerPayload"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class SubCommand60ServerAttribute : WireDataContractBaseLinkAttribute
	{
		/// <inheritdoc />
		public SubCommand60ServerAttribute(SubCommand60OperationCode opCode)
			: base((int)opCode, typeof(BlockNetworkCommandEventClientPayload))
		{
			if(!Enum.IsDefined(typeof(SubCommand60OperationCode), opCode)) throw new InvalidEnumArgumentException(nameof(opCode), (int)opCode, typeof(SubCommand60OperationCode));
		}
	}
}
