using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/*  uint8_t client_id;
		uint8_t unused;
		uint32_t unk;
		float w;
		float x;
		float y;
		float z;
	*/

	//Syl: https://github.com/Sylverant/ship_server/blob/b3bffc84b558821ca2002775ab2c3af5c6dde528/src/subcmd.h#L439
	//Tethella: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L8356
	/// <summary>
	/// Subcommand payload that will teleport a client to the specified location.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.TeleportToPosition)]
	public sealed class Sub60TeleportToPositionCommand : BaseSubCommand60
	{
		//TODO: Refactor this into an interface or something
		//This is a short to absorb the unused byte
		[WireMember(1)]
		public short ClientId { get; }

		//TODO: What is this? I think it's a position checksum.
		[WireMember(2)]
		private uint unused { get; }

		//TODO: When we figure out what this is maybe add it back to Vector4
		[WireMember(3)]
		private float w { get; }

		//TODO: The Vector3 may be misordered. Is xyzw but we may need wxyz
		/// <summary>
		/// The position to teleport to.
		/// </summary>
		[WireMember(4)]
		public Vector3<float> Position { get; }

		/// <inheritdoc />
		public Sub60TeleportToPositionCommand(short clientId, [NotNull] Vector3<float> position)
			: this()
		{
			if(position == null) throw new ArgumentNullException(nameof(position));

			ClientId = clientId;
			Position = position;
		}

		public Sub60TeleportToPositionCommand()
		{
			//Calc static 32bit size
			CommandSize = 24 / 4;
		}
	}
}
