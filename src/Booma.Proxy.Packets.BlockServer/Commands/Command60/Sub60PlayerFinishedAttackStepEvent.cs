using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	//https://sylverant.net/wiki/index.php/Packet_0x60#Subcommand_0x46
	/// <summary>
	/// Packet sent when an attack step is completed by a client.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.AttackStepFinished)]
	public sealed class Sub60PlayerFinishedAttackStepEvent : BaseSubCommand60, IMessageContextIdentifiable
	{
		//TODO: I haven't verified that this is the identifier for the client BUT it probably has to be.
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; }

		[WireMember(2)]
		private byte unk1 { get; }
		
		[SendSize(SendSizeAttribute.SizeType.UShort)]
		[WireMember(3)]
		private AttackHitResults[] _HitResults { get; }

		/// <summary>
		/// The results of an attack hit.
		/// </summary>
		public IEnumerable<AttackHitResults> HitResults => _HitResults;

		/// <summary>
		/// Indicates if the attack missed.
		/// </summary>
		public bool isAttackMissed => _HitResults == null || _HitResults.Length == 0;

		//TODO: What is the last two bytes of this packet? Seems always 0, not from crypto padding.
		[WireMember(4)]
		private short unk2 { get; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private Sub60PlayerFinishedAttackStepEvent()
		{
			
		}
	}
}
