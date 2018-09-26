namespace Booma.Proxy
{
	public sealed partial class CapturedPacketsTests
	{
		public class PacketCaptureTestEntry
		{
			public short OpCode { get; }

			public byte[] BinaryData { get; }

			public string FileName { get; }

			/// <inheritdoc />
			public PacketCaptureTestEntry(short opCode, byte[] binaryData, string fileName)
			{
				OpCode = opCode;
				BinaryData = binaryData;
				FileName = fileName;
			}

			/// <inheritdoc />
			public override string ToString()
			{
				//Special naming for 0x60 to make it easier to search
				if(OpCode == 0x60)
					return FileName.Replace("0x60_", $"0x60_0x{BinaryData[6]:X}_");

				return $"{FileName}";
			}

			/// <inheritdoc />
			public override int GetHashCode()
			{
				return FileName.GetHashCode();
			}
		}
	}
}
