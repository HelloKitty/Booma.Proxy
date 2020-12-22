using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x43
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.AttackStepOne)]
	public sealed class Sub60PlayerAttackStepOneCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; }

		//Unknown 1 byte always follows id
		[WireMember(2)]
		private byte unk1 { get; }

		//TODO: The calculation we're doing to init or parse this is wrong. It does not match the binary packet
		//TODO: We must figure out how to exactly compute this value, test cases show off by 1 bit.
		/// <summary>
		/// The raw rotation value
		/// that is sent over the network.
		/// </summary>
		[WireMember(3)]
		private short RawNetworkRotation { get; set; }

		/// <summary>
		/// The rotation about the Y-axis.
		/// </summary>
		public float YAxisRotation => RawNetworkRotation.FromNetworkRotationToYAxisRotation();

		//TODO: Is this anything but padding?
		[WireMember(4)]
		private short unk2 { get; }

		/// <inheritdoc />
		public Sub60PlayerAttackStepOneCommand(byte identifier, float yAxisRotation)
			: this()
		{
			Identifier = identifier;

			RawNetworkRotation = yAxisRotation.ToNetworkRotation();
		}

		//Serializer ctor
		private Sub60PlayerAttackStepOneCommand()
		{
			CommandSize = 8 / 4;
		}
	}
}
