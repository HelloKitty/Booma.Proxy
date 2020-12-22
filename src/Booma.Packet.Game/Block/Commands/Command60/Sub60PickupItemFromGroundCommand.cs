using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	//Link Syl and Teth
	//Sylverant: https://github.com/Sylverant/ship_server/blob/da29a8e0ffbb394bd7cad462024e68df3909d528/src/subcmd.h#L360
	/// <summary>
	/// Subcommand payload sent when an item is picked up from the ground.
	/// </summary>
	[WireDataContract]
	[SubCommand60(SubCommand60OperationCode.PickupItem)]
	public sealed class Sub60PickupItemFromGroundCommand : BaseSubCommand60, IMessageContextIdentifiable
	{
		/// <inheritdoc />
		[WireMember(1)]
		public byte Identifier { get; internal set; }

		//TODO: Is this zone id?
		[WireMember(2)]
		public byte Unknown1 { get; internal set; }

		//TODO: Why is the identifier sent twice?
		[WireMember(3)]
		public byte Identifier2 { get; internal set; }

		//TODO: Is this zone id?
		[WireMember(4)]
		public byte Unknown2 { get; internal set; }

		/// <summary>
		/// The zone this item is being dropped in.
		/// </summary>
		[WireMember(5)]
		public short ZoneId { get; internal set; }

		/// <summary>
		/// The ID of the item.
		/// </summary>
		[WireMember(6)]
		public uint ItemId { get; internal set; }
		//TODO: Ctor

		/// <inheritdoc />
		public Sub60PickupItemFromGroundCommand()
		{
			//TODO: Command size
		}
	}
}
