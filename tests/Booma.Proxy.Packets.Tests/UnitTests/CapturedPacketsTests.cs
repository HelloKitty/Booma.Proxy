﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using Generic.Math;
using NUnit.Framework;
using Reinterpret.Net;

namespace Booma
{
	public sealed partial class CapturedPacketsTests
	{
		public static SerializerService Serializer { get; protected set; } = new SerializerService();

		public static List<PacketCaptureTestEntry> ClientPacketCapturesSource { get; } = BuildCaptureEntries(true);

		public static List<PacketCaptureTestEntry> ServerPacketCapturesSource { get; } = BuildCaptureEntries(false);

		private static List<PacketCaptureTestEntry> BuildCaptureEntries(bool isSearchingForClient)
		{
			List<PacketCaptureTestEntry> testSource = new List<PacketCaptureTestEntry>(5000);

			//Do file loading first because it could fail
			//Then we need to populate the input source for the tests.
			//TODO: Expose a better way for other contributors to deal with this
			string testPath = Path.Combine(@"C:\Users\Glader\Documents\GitHub\Booma.Proxy\data", "Packets", isSearchingForClient ? "Client" : "Server");

			//Each directory should fit the form of OpCode name
			//Even though the below is a Path with the directory if we pretend it's a file we can easily get the last part of the path
			//which represents the opcode
			string[] files = null;

			try
			{
				files = Directory.GetFiles(testPath);
			}
			catch(Exception ee)
			{
				throw new InvalidOperationException($"Failed to load File: {testPath} Exception: {ee.Message} Stack: {ee.StackTrace}");
			}

			//Now we want to load each capture.
			foreach(var cap in files)
			{
				string filePath = Path.Combine(testPath, cap);
				//Captures should have a guid on the end of them

				string guidString = cap.Split('_').Last().Replace(".packet", "");
				Guid guid;
				try
				{
					guid = Guid.Parse(guidString);
				}
				catch(Exception e)
				{
					Console.WriteLine();
					throw new InvalidOperationException($"Failed to create guid from String: {guidString} Error: {e.Message}", e);
				}

				try
				{
					byte[] bytes = File.ReadAllBytes(filePath);

					//First 2 bytes will have the short opcode so we can just reintrepet to get them
					testSource.Add(new PacketCaptureTestEntry(bytes.Reinterpret<short>(), bytes, Path.GetFileName(cap)));
				}
				catch(Exception e)
				{
					throw new InvalidOperationException($"Failed to open File: {filePath} Exception: {e.Message}", e);
				}
			}

			return testSource;
		}

		static CapturedPacketsTests()
		{
			Serializer.RegisterGamePacketSerializers()
				.RegisterPatchPacketSerializers();
		}

		[Test]
		[TestCaseSource(nameof(ClientPacketCapturesSource))]
		public void Can_Deserialize_ClientCaptures_To_ClientPayloads(PacketCaptureTestEntry entry)
		{
			Generic_CanDeserialize_CaptureTest<PSOBBGamePacketPayloadClient, GameNetworkOperationCode>(entry);
		}

		[Test]
		[TestCaseSource(nameof(ServerPacketCapturesSource))]
		public void Can_Deserialize_ClientCaptures_To_ServerPayloads(PacketCaptureTestEntry entry)
		{
			Generic_CanDeserialize_CaptureTest<PSOBBGamePacketPayloadServer, GameNetworkOperationCode>(entry);
		}

		private void Generic_CanDeserialize_CaptureTest<TBasePayloadType, TOperationType>(PacketCaptureTestEntry entry)
			where TBasePayloadType : IOperationCodeable<TOperationType>, ITypeSerializerReadingStrategy<TBasePayloadType>
			where TOperationType : Enum
		{
			//arrange
			SerializerService serializer = Serializer;
			TBasePayloadType payload;

			//act
			try
			{
				payload = serializer.Deserialize<TBasePayloadType>(entry.BinaryData, 0);

				if(payload is IUnknownPayloadType)
				{
					Assert.Warn($"Encountered unimplemented OpCode: 0x{payload.OperationCode:X}.");
					return;
				}
			}
			catch(Exception e)
			{
				Assert.Fail($"Critical failure. Cannot deserialize File: {entry.FileName} FileSize: {entry.BinaryData.Length} \n\n Exception: {e.Message} Stack: {e.StackTrace}");
				return;
			}
			finally
			{

			}

			//assert
			Assert.NotNull(payload, $"Resulting capture capture deserialization attempt null for File: {entry.FileName}");
			//We should have deserialized it. We want to make sure the opcode matches
			Assert.AreEqual(entry.OpCode, ConvertPayloadOperationCode<TBasePayloadType, TOperationType>(payload), $"Mismatched {nameof(payload.OperationCode)} on packet capture File: {entry.FileName}. Expected: {entry.OpCode} Was: {payload.OperationCode}");
		}

		private static short ConvertPayloadOperationCode<TBasePayloadType, TOperationType>(TBasePayloadType payload) 
			where TBasePayloadType : IOperationCodeable<TOperationType>, ITypeSerializerReadingStrategy<TBasePayloadType> 
			where TOperationType : Enum
		{
			return GenericMath.Convert<TOperationType, short>(payload.OperationCode);
		}

		public void Generic_Can_Serialize_DeserializedClientDTO_To_Same_Binary_Representation<TBasePayloadType, TOperationCodeType>(PacketCaptureTestEntry entry)
			where TBasePayloadType : IOperationCodeable<TOperationCodeType>, ITypeSerializerReadingStrategy<TBasePayloadType>, ITypeSerializerWritingStrategy<TBasePayloadType>
			where TOperationCodeType : Enum
		{
			//arrange
			Console.WriteLine($"Entry Size: {entry.BinaryData.Length} OpCode: {entry.OpCode}");
			SerializerService serializer = Serializer;
			TBasePayloadType payload = default;
			int readOffset = 0;

			try
			{
				payload = serializer.Read<TBasePayloadType>(entry.BinaryData, ref readOffset);
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine($"Offset: {readOffset} BinarySize: {entry.BinaryData.Length}");
				throw;
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e}");
				throw;
			}

			if(payload is IUnknownPayloadType)
			{
				Assert.Warn($"Encountered unimplemented OpCode: 0x{payload.OperationCode:X} - {payload.OperationCode.ToString()}.");
				return;
			}

			bool isSub60 = false;
			bool isSub62 = false;
			//Special handling for 0x60 payloads, we don't want to deal with unknown subcommands. Just warn
			if(payload is ISub60CommandContainer sub60)
			{
				isSub60 = true;
				if(sub60.Command is UnknownSubCommand60Command)
				{
					Assert.Warn($"Encountered unimplemented Sub60 SubCommand: 0x{payload.OperationCode:X} 0x{entry.BinaryData[6]:X}.");
					return;
				}
			}
			else if(payload is ISub62CommandContainer sub62)
			{
				isSub62 = true;
				if(sub62.Command is UnknownSubCommand62Command)
				{
					Assert.Warn($"Encountered unimplemented Sub62 SubCommand: 0x{payload.OperationCode:X} 0x{entry.BinaryData[6]:X}.");
					return;
				}
			}

			//act
			byte[] rentedBuffer = ArrayPool<byte>.Shared.Rent(36000 * 2);

			//Must zero out for cleanest tests.
			for (int i = 0; i < rentedBuffer.Length; i++)
				rentedBuffer[i] = default(byte);

			try
			{
				
				Span<byte> buffer = new Span<byte>(rentedBuffer);

				int offset = 0;
				serializer.Write(payload, buffer, ref offset);
				buffer = buffer.Slice(0, offset);

				//The plus 2 is from the packet size which is included as calculation in whether it is appropriate for the block
				//size of 8.
				//+2 is also added to the original binary length since it's missing the packet size too.
				int serializedBytesWithBlockSize = ConvertToBlocksizeCount(buffer.Length + 2, 8);
				int entryBytesWithBlockSize = ConvertToBlocksizeCount(entry.BinaryData.Length + 2, 8);
				//assert
				try
				{
					if(!isSub60 && !isSub62)
						//convert the serialized bytes to block size since Sylverant and the packet captures will include that in the data
						Assert.AreEqual(entryBytesWithBlockSize, serializedBytesWithBlockSize, $"Mismatched length on OpCode: {(GameNetworkOperationCode)entry.OpCode} - 0x{entry.OpCode:X} Type: {payload.GetType().Name}");
					else if(isSub60)
					{
						var command = (payload as ISub60CommandContainer).Command;
						//Similar to the above but we include information about the sub60 command
						Assert.AreEqual(entryBytesWithBlockSize, serializedBytesWithBlockSize, $"Mismatched length on OpCode: {(GameNetworkOperationCode)entry.OpCode} - 0x{entry.OpCode:X} Type: {payload.GetType().Name} Sub60 OpCode: 0x{entry.BinaryData[6]:X} Type: {command.GetType().Name}");

						//Command 60s should also check the command size
						Assert.AreEqual(entry.BinaryData[7], buffer[7], $"Mismatched Sub60 {nameof(BaseSubCommand60.CommandSize)} on OpCode: {(GameNetworkOperationCode)entry.OpCode} - 0x{entry.OpCode:X} Type: {payload.GetType().Name} Sub60 OpCode: 0x{entry.BinaryData[6]:X} Type: {command.GetType().Name}");
					}
					else if(isSub62)
					{
						var command = (payload as ISub62CommandContainer).Command;
						//Similar to the above but we include information about the sub60 command
						Assert.AreEqual(entryBytesWithBlockSize, serializedBytesWithBlockSize, $"Mismatched length on OpCode: {(GameNetworkOperationCode)entry.OpCode} - 0x{entry.OpCode:X} Type: {payload.GetType().Name} Sub62 OpCode: 0x{entry.BinaryData[6]:X} Type: {command.GetType().Name}");
					}
				}
				catch(AssertionException e)
				{
					Assert.Fail($"Failed: {e.Message} {PrintFailureBytes(entry.BinaryData, buffer)}");
				}

				//check both lengths since we accept that some packet models won't include the padding.
				for(int i = 0; i < entry.BinaryData.Length && i < buffer.Length; i++)
				{
					if (!isSub60 && !isSub62)
					{
						//This is an optimization to avoid pointless string allocations/building.
						if(entry.BinaryData[i] != buffer[i])
							Assert.AreEqual(entry.BinaryData[i], buffer[i], $"Mismatched byte value at Index: {i} on OpCode: 0x{entry.OpCode:X} Type: {payload.GetType().Name} {PrintFailureBytes(entry.BinaryData, buffer)}");
					}
					else if(isSub60)
					{
						var command = (payload as ISub60CommandContainer).Command;

						//This is an optimization to avoid pointless string allocations/building.
						if(entry.BinaryData[i] != buffer[i])
							Assert.AreEqual(entry.BinaryData[i], buffer[i], $"Mismatched byte value at Index: {i} on OpCode: 0x{entry.OpCode:X} Type: {payload.GetType().Name} Sub60 OpCode: 0x{entry.BinaryData[6]:X} Type: {command.GetType().Name} {PrintFailureBytes(entry.BinaryData, buffer)}");
					}
					else if(isSub62)
					{
						var command = (payload as ISub62CommandContainer).Command;

						//This is an optimization to avoid pointless string allocations/building.
						if(entry.BinaryData[i] != buffer[i])
							Assert.AreEqual(entry.BinaryData[i], buffer[i], $"Mismatched byte value at Index: {i} on OpCode: 0x{entry.OpCode:X} Type: {payload.GetType().Name} Sub62 OpCode: 0x{entry.BinaryData[6]:X} Type: {command.GetType().Name} {PrintFailureBytes(entry.BinaryData, buffer)}");
					}
				}

				//Special check for when we have a packet that hasn't failed but has differently lenghts (assumed to be blocksize)
				//we check and make sure that the ending bytes are actually 0. If they aren't it likely NOT padding and additional unhandled data
				if(entry.BinaryData.Length > buffer.Length)
					for(int i = buffer.Length; i < entry.BinaryData.Length; i++)
						if(!isSub60 && !isSub62)
						{
							Assert.AreEqual(0, entry.BinaryData[i], $"Encountered assumed padding byte at Index: {i} on OpCode: 0x{entry.OpCode} Type: {payload.GetType().Name} but value was: 0x{entry.BinaryData[i]:X}");
						}
						else if(isSub60)
						{
							var command = (payload as ISub60CommandContainer).Command;
							Assert.AreEqual(0, entry.BinaryData[i], $"Encountered assumed padding byte at Index: {i} on OpCode: 0x{entry.OpCode} Type: {payload.GetType().Name} but value was: 0x{entry.BinaryData[i]:X} Sub60 OpCode: 0x{entry.BinaryData[6]:X} Type: {command.GetType().Name}");
						}
						else if(isSub62)
						{
							var command = (payload as ISub62CommandContainer).Command;
							Assert.AreEqual(0, entry.BinaryData[i], $"Encountered assumed padding byte at Index: {i} on OpCode: 0x{entry.OpCode} Type: {payload.GetType().Name} but value was: 0x{entry.BinaryData[i]:X} Sub62 OpCode: 0x{entry.BinaryData[6]:X} Type: {command.GetType().Name}");
						}
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(rentedBuffer);
			}
		}

		[Test]
		[TestCaseSource(nameof(ClientPacketCapturesSource))]
		public void Can_Serialize_DeserializedClientDTO_To_Same_Binary_Representation(PacketCaptureTestEntry entry)
		{
			Generic_Can_Serialize_DeserializedClientDTO_To_Same_Binary_Representation<PSOBBGamePacketPayloadClient, GameNetworkOperationCode>(entry);
		}

		[Test]
		[TestCaseSource(nameof(ServerPacketCapturesSource))]
		public void Can_Serialize_DeserializedServerDTO_To_Same_Binary_Representation(PacketCaptureTestEntry entry)
		{
			Generic_Can_Serialize_DeserializedClientDTO_To_Same_Binary_Representation<PSOBBGamePacketPayloadServer, GameNetworkOperationCode>(entry);
		}

		public static string PrintFailureBytes(byte[] original, Span<byte> result)
		{
			//if(original.Length > 300)
			//	return $"Original bytes too long to log. Size: {original.Length}";
			StringBuilder builder = new StringBuilder(original.Length * 3 + result.Length * 3 + 500);
			builder.Append($"Original bytes: \n");

			for (int i = 0; i < original.Length; i++)
				builder.Append($" {original[i]:X2}");

			builder.Append($"\n\n Result: \n");

			for(int i = 0; i < result.Length; i++)
				builder.Append($" {result[i]:X2}");

			return builder.ToString();
		}

		//From GladNet block cipher implementation, will compute the blocksize of something.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int ConvertToBlocksizeCount(int count, int blockSize)
		{
			int remainder = count % blockSize;

			//Important to check if it's already perfectly size
			//otherwise below code will return count + blocksize
			if(remainder == 0)
				return count;

			return count + (blockSize - (count % blockSize));
		}
	}
}
