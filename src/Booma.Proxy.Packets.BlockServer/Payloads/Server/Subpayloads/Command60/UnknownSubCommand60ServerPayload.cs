using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// An unimplemented or unknown subcommand for the <see cref="BlockNetworkCommandEventServerPayload"/>.
	/// </summary>
	[WireDataContract]
	public sealed class UnknownSubCommand60ServerPayload : BlockNetworkCommandEventServerPayload, IUnknownPayloadType
	{
		/// <inheritdoc />
		public new short OperationCode => (short)base.SubcommandOperationCode;

		/// <inheritdoc />
		[ReadToEnd]
		[WireMember(1)]
		public byte[] UnknownBytes { get; } = new byte[0]; //readtoend requires at least an empty array init

		private UnknownSubCommand60ServerPayload()
		{
			
		}

		/// <inheritdoc />
		public override string ToString()
		{
			if(Enum.IsDefined(typeof(SubCommand60OperationCode), (byte)OperationCode))
				return $"Unknown SubCommand60: {OperationCode:X} Name: {((SubCommand60OperationCode)OperationCode).ToString()} Type: {base.ToString()} Size: {4 + UnknownBytes.Length + 2 + 2}";
			else
				return $"Unknown SubCommand60: {OperationCode:X} Type: {base.ToString()} Size: {4 + UnknownBytes.Length + 2 + 2}";
		}
	}
}
