using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Simple;
using GladNet;
using Moq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Internal.VR;

namespace Booma.Proxy.Tests
{
	//Root caus of Crash 1 was stackoverflow caused by SlotIndex property of LocalPlayerDataInitializable

	//BlockOtherClientFinishedWarpingEventPayloadHandler causing runtime/editor crashes.
	[TestFixture]
	public static class CrashTest1
	{
		[Test]
		public static async Task Test_CrashTest1_BlockOtherClientFinishedWarpingEventPayloadHandler()
		{
			//arrange
			//player data
			var localPlayerDataInitializable = new LocalPlayerDataInitializable(new StaticCharacterSlotModel(0), new EntityGuidDictionary<GameObject>());
			EntityGuidDictionary<PlayerZoneData> playerZoneDatas = new EntityGuidDictionary<PlayerZoneData>();

			BlockOtherClientFinishedWarpingEventPayloadHandler handler = new BlockOtherClientFinishedWarpingEventPayloadHandler(new DefaultPSOScaleUnitScalerStrategy(), new StaticZoneSettings(15), new ConsoleOutLogger("Console", LogLevel.All, true, true, true, String.Empty), localPlayerDataInitializable, playerZoneDatas);

			//arrange
			await handler.HandleMessage(new DefaultPeerMessageContext<PSOBBGamePacketPayloadClient>(Mock.Of<IConnectionService>(), Mock.Of<IPeerPayloadSendService<PSOBBGamePacketPayloadClient>>(), Mock.Of<IPeerRequestSendService<PSOBBGamePacketPayloadClient>>()), new BlockNetworkCommand60EventServerPayload(new Sub60FinishedWarpingBurstingCommand(1)));
		}

		[Test]
		public static void Test_LocalPlayerDataInitializable_DoesNotCauseStackOverflow()
		{
			var localPlayerDataInitializable = new LocalPlayerDataInitializable(new StaticCharacterSlotModel(0), new EntityGuidDictionary<GameObject>());

			Assert.DoesNotThrow(() =>
			{
				int i = localPlayerDataInitializable.SlotIndex;
			});
		}

		private class StaticCharacterSlotModel : ICharacterSlotSelectedModel
		{
			/// <inheritdoc />
			public byte SlotSelected { get; set; }

			/// <inheritdoc />
			public StaticCharacterSlotModel(byte slotSelected)
			{
				SlotSelected = slotSelected;
			}
		}

		private class StaticZoneSettings : IZoneSettings
		{
			/// <inheritdoc />
			public short ZoneId { get; }

			/// <inheritdoc />
			public StaticZoneSettings(short zoneId)
			{
				ZoneId = zoneId;
			}
		}
	}
}
