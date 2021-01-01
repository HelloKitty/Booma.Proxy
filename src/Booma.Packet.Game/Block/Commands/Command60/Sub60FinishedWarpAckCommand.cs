using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	//TODO: What is this?
	/// <summary>
	/// Command sent to set position and alert other clients
	/// when they finish warping.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.AlertFreshlyWarpedClients)]
	public sealed partial class Sub60FinishedWarpAckCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		[WireMember(2)]
		internal byte unusued { get; set; }

		/// <summary>
		/// The id for the zone that the client is in.
		/// </summary>
		[WireMember(3)]
		public int ZoneId { get; internal set; }

		/// <summary>
		/// The position the client has moved to.
		/// </summary>
		[WireMember(4)]
		public Vector3<float> Position { get; internal set; } //server should set X and Z, ignoring y.

		[WireMember(5)]
		internal short RawRotation { get; set; }

		//TODO: Soly said this is rotation so we should handle it 65536f / 360f
		//[WireMember(5)]
		public float YAxisRotation => RawRotation.FromNetworkRotationToYAxisRotation();

		//There are 2 extra bytes here at the end and are FF FF
		[WireMember(6)]
		internal ushort unk { get; set; } = ushort.MaxValue; 

		public Sub60FinishedWarpAckCommand(byte clientId, int zoneId, [NotNull] Vector3<float> position, float yAxisRotation)
			: this()
		{
			if(position == null) throw new ArgumentNullException(nameof(position));
			if(zoneId < 0) throw new ArgumentOutOfRangeException(nameof(zoneId));

			Identifier = clientId;
			ZoneId = zoneId;
			Position = position;

			RawRotation = yAxisRotation.ToNetworkRotation();
		}

		public Sub60FinishedWarpAckCommand(int clientId, int zoneId, [NotNull] Vector3<float> position, float yAxisRotation)
			: this((byte)clientId, zoneId, position, yAxisRotation)
		{

		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public Sub60FinishedWarpAckCommand()
			: base(SubCommand60OperationCode.AlertFreshlyWarpedClients)
		{
			//Calc static 32bit size
			CommandSize = 24 / 4;
		}
	}
}
