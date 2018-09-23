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
