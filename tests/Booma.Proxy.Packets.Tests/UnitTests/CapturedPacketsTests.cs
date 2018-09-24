using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using GladNet;
using NUnit.Framework;
using Reinterpret.Net;

namespace Booma.Proxy
{
	public sealed partial class CapturedPacketsTests
	{
		public static SerializerService Serializer { get; protected set; } = new SerializerService();

		public static List<PacketCaptureTestEntry> ClientPacketCapturesSource { get; } = BuildCaptureEntries(true);

		public static List<PacketCaptureTestEntry> ServerPacketCapturesSource { get; } = BuildCaptureEntries(false);

		private static List<PacketCaptureTestEntry> BuildCaptureEntries(bool isSearchingForClient)
		{
			List<PacketCaptureTestEntry> testSource = new List<PacketCaptureTestEntry>(500);

			//Do file loading first because it could fail
			//Then we need to populate the input source for the tests.
			string testPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Packets", isSearchingForClient ? "Client" : "Server");

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
			foreach(Type t in AllPacketsTests.PayloadTypes)
			{
				Serializer.RegisterType(t);
			}

			Serializer.Compile();
		}

		[Test]
		[TestCaseSource(nameof(ClientPacketCapturesSource))]
		public void Can_Deserialize_ClientCaptures_To_ClientPayloads(PacketCaptureTestEntry entry)
		{
			Generic_CanDeserialize_CaptureTest<PSOBBGamePacketPayloadClient>(entry);
		}

		[Test]
		[TestCaseSource(nameof(ServerPacketCapturesSource))]
		public void Can_Deserialize_ClientCaptures_To_ServerPayloads(PacketCaptureTestEntry entry)
		{
			Generic_CanDeserialize_CaptureTest<PSOBBGamePacketPayloadServer>(entry);
		}

		private void Generic_CanDeserialize_CaptureTest<TBasePayloadType>(PacketCaptureTestEntry entry)
			where TBasePayloadType : IPacketPayload, IOperationCodeable
		{
			//arrange
			SerializerService serializer = Serializer;
			TBasePayloadType payload;

			//act
			try
			{
				payload = serializer.Deserialize<TBasePayloadType>(entry.BinaryData);

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
			Assert.AreEqual(entry.OpCode, payload.OperationCode, $"Mismatched {nameof(payload.OperationCode)} on packet capture File: {entry.FileName}. Expected: {entry.OpCode} Was: {payload.OperationCode}");
		}

		public void Generic_Can_Serialize_DeserializedClientDTO_To_Same_Binary_Representation<TBasePayloadType>(PacketCaptureTestEntry entry)
			where TBasePayloadType : IPacketPayload, IOperationCodeable
		{
			//arrange
			Console.WriteLine($"Entry Size: {entry.BinaryData.Length} OpCode: {entry.OpCode}");
			SerializerService serializer = Serializer;
			TBasePayloadType payload = serializer.Deserialize<TBasePayloadType>(entry.BinaryData);

			if(payload is IUnknownPayloadType)
			{
				Assert.Warn($"Encountered unimplemented OpCode: 0x{payload.OperationCode:X} - {(GameNetworkOperationCode)payload.OperationCode}.");
				return;
			}

			bool isSub60 = false;
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

			//act
			byte[] serializedBytes = serializer.Serialize(payload);

			//assert
			try
			{
				if(!isSub60)
					Assert.AreEqual(entry.BinaryData.Length, serializedBytes.Length, $"Mismatched length on OpCode: {(GameNetworkOperationCode)entry.OpCode} - 0x{entry.OpCode:X} Type: {payload.GetType().Name}");
				else
				{
					var command = (payload as ISub60CommandContainer).Command;
					//Similar to the above but we include information about the sub60 command
					Assert.AreEqual(entry.BinaryData.Length, serializedBytes.Length, $"Mismatched length on OpCode: {(GameNetworkOperationCode)entry.OpCode} - 0x{entry.OpCode:X} Type: {payload.GetType().Name} Sub60 OpCode: 0x{entry.BinaryData[6]:X} Type: {command.GetType().Name}");
				}
			}
			catch(AssertionException e)
			{
				Assert.Fail($"Failed: {e.Message} {PrintFailureBytes(entry.BinaryData, serializedBytes)}");
			}


			for(int i = 0; i < entry.BinaryData.Length; i++)
			{
				if(!isSub60)
					Assert.AreEqual(entry.BinaryData[i], serializedBytes[i], $"Mismatched byte value at Index: {i} on OpCode: {entry.OpCode} Type: {payload.GetType().Name}");
				else
				{
					var command = (payload as ISub60CommandContainer).Command;
					Assert.AreEqual(entry.BinaryData[i], serializedBytes[i], $"Mismatched byte value at Index: {i} on OpCode: {entry.OpCode} Type: {payload.GetType().Name} Sub60 OpCode: 0x{entry.BinaryData[6]:X} Type: {command.GetType().Name}");
				}
			}
		}

		[Test]
		[TestCaseSource(nameof(ClientPacketCapturesSource))]
		public void Can_Serialize_DeserializedClientDTO_To_Same_Binary_Representation(PacketCaptureTestEntry entry)
		{
			Generic_Can_Serialize_DeserializedClientDTO_To_Same_Binary_Representation<PSOBBGamePacketPayloadClient>(entry);
		}

		[Test]
		[TestCaseSource(nameof(ServerPacketCapturesSource))]
		public void Can_Serialize_DeserializedServerDTO_To_Same_Binary_Representation(PacketCaptureTestEntry entry)
		{
			Generic_Can_Serialize_DeserializedClientDTO_To_Same_Binary_Representation<PSOBBGamePacketPayloadServer>(entry);
		}

		public static string PrintFailureBytes(byte[] original, byte[] result)
		{
			return $"Original bytes: {original.Aggregate("", (s, b) => $"{s} {b:X}")} Result: {result.Aggregate("", (s, b) => $"{s} {b:X}")}";
		}
	}
}
