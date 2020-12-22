using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	[WireDataContract]
	[SubCommand6D(SubCommand6DOperationCode.PlayerJoinedData)]
	public sealed class Sub6DFakePlayerJoinDataNeededCommand : BaseSubCommand6D
	{
		//See: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L11090
		//This is a lot of different types of data here
		//the issue is, I don't know how long it's actually suppose to be.
		[KnownSize(0x4C0 - 8)]
		[WireMember(2)]
		public byte[] UnknownBytes { get; } = new byte[0x4C0 - 8];

		public Sub6DFakePlayerJoinDataNeededCommand(byte identifier)
			: base(identifier)
		{
			CommandSize = 0x4C0;

			//UnknownBytes = binaryDump;

			//Teth will check Tech data which is a signed byte I guess
			//It needs to have -1 in all fields for test purposes
			//From Teth: https://github.com/justnoxx/psobb-tethealla/blob/master/ship_server/ship_server.c#L11109
			for(int ch = 0; ch < 19; ch++)
			{
				//if ((char) client->decryptbuf[0xC4+ch] > max_tech_level[ch][client->character._class])
				UnknownBytes[0xC4 - 8 + ch] = byte.MaxValue;
			}

			//UnknownBytes[0xC4 - 8 - 2] = 0x60;
			//UnknownBytes[0xC4 - 8 - 1] = 0x01;

			//UnknownBytes[0x9E - 6] = 0xFF;
			//UnknownBytes[0x9E - 6 + 1] = 0xFF;

			//I think first byte is also identifier
			UnknownBytes[0] = identifier;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected Sub6DFakePlayerJoinDataNeededCommand()
		{

		}

		static Sub6DFakePlayerJoinDataNeededCommand()
		{
			binaryDump = System.Convert.FromBase64String(dumped0x60_0x70);

			if(binaryDump.Length != (0x4C0 - 8))
				throw new InvalidOperationException($"Incorrect Size: {binaryDump.Length} in Faked 0x6D 0x70 packet");
		}

		private static byte[] binaryDump;

		private static string dumped0x60_0x70 = "AQAAAAAAABA9AGdDAAAAAGaAhUMAAAAAAMb//wAAAAABACgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAgd6AAgAAAAAAAAAA//8AAAAAAAAAAAAAAAAAAAAAAAAAAAAA//8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEGIB/////////////////////////wAgIDQyMDAwMDAxAAAAAAAAAAAAAAAAAAD3lB3/AAAAAAAAAAAAAAAAAAAAAAAAAAAGAAADJQAAAAAAAQAAAAAAAACFAEkAKQCrqqo+AAAAPwkARQBHAGwAYQBkAGUAcgAAAAAAAAAAAOXxCgAAAAAAIwAdAC0AFAARAB4ACgAAAAAAjEEAACBBAAAAAAAAAAAxAQAACAAAAAIAAABMAAAAAAEAAAAAAAAAAAAAUQAhAAAAAAACAAAATAAAAAEBAAAAAAAAAAAAAFIAIQAAAAAAAgAAAEwAAAACAAUA9AEAAAAAAABTACEAFAAAAAEAAABQAAAAAwAAAAAFAAAAAAAAVAAhAAAAAAABAAAAUAAAAAMBAAAAAgAAAAAAAFUAIQAAAAAAAQAAAEQAAAABAQAAAAABAAEAAABWACEAAAAAAAEAAABQAAAAAwIAAAAAAAAAAAAAVwAhAAAAAAABAAAARAAAAAAGAAAAAAAAAAAAAFgAIQAAAAAAVAAAABgAaACtAG8ARQCwACwAVwCdAGIALgCdAGUAAACAAGYAKgB+AAkAZgAqAH4AZgAqAH4AYgAuAAAAEAAeAJQADwAqAKwAdQAaAKAAFQA2AMsAcwAAAMEAUQBWAPkAAwA4AGUAAwAOAGQAAAAMALAAAAAEABUANgDLAG8ARQCwAA8AKgCsAGIALgCdAAAABwCWAB8ANwBlACcAAwBLAB0ABQApACcAAQAAAB0AAgADACIABQBlAFUAgAAsAFcAnQBtAHQAAAAqAHAAnABsAHwAgQAJABsAbwB2ABMAfAB5AAAAcQBqAE4AhAB5AEUAjwBqACMAkQB2ADUArQAAADIAqwBtAF8A2ABrAAoAAgADACIAAAAEAAcAAAAAAB0ALQABAA0AAwBLAB0ALgBIAA0ABwCWAB8ALwCXAAwACADjAB8AMADkAAwABwB3ABUAZwAnABkAbQBNACMAZABhACcAawA0AFMAZAAzAFUAbQAbAG8AdgALACEA5wBtAHYA6wBnABoA3QBkACQA6gAgAEsAtwAlAEoApwAXAEwAgAAlAB0AWQAXABwASQAlABIAKQATABEAFgAgAAsAGQB8AJkAGQB8AJkATwCEAJkAUACEAKMAKACQAJ0AHwCYAK0AMQCpAJ4AbgC7ALAAYADSAJ0AHgDWAKsAKQDiAJQANgBrAIQAgQBsAHwAgQAZAHwAmQAqAHAAnAAiAHwAowAYAGgArQAWAHsArAArAHsAtgAXAIUAtgBUAIgAxAAfAJgArQBDAK0AxABuALsAsAAgAMoAygAeANYAqwB0AOYAoAApAOIAlAAEAAAAAAAAAAAAAAAAAAAAAAA=";
	}
}
