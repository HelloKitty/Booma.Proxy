using System.Linq;

namespace Booma
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
				//Special naming for subcommand to make it easier to search
				//We need to support both new captures that automatically have this AND legacy captures which do not
				if(OpCode == 0x60)
					if(FileName.ToCharArray().Count(c => c == '_') < 2)
						return FileName.Replace("0x60_", $"0x60_0x{(int)(BinaryData[6]):X2}_");

				if(OpCode == 0x62)
					if(FileName.ToCharArray().Count(c => c == '_') < 2)
						return FileName.Replace("0x62_", $"0x62_0x{(int)(BinaryData[6]):X2}_");

				if(OpCode == 0x6D)
					if(FileName.ToCharArray().Count(c => c == '_') < 2)
						return FileName.Replace("0x6D_", $"0x6D_0x{(int)(BinaryData[6]):X2}_");

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
