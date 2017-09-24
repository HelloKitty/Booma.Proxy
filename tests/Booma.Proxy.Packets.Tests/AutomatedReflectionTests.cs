using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using NUnit.Framework;

namespace Booma.Proxy.Packets.Tests
{
	[TestFixture]
	public class AutomatedReflectionTests
	{
		private static IEnumerable<Type> PatchPayloadTypes = typeof(PatchingDoneCommandPayload).Assembly.GetTypes()
			.Where(t => typeof(PSOBBPatchPacketPayload).IsAssignableFrom(t));

		[Test]
		[TestCaseSource(nameof(PatchPayloadTypes))]
		public static void Test_Can_Register_All_Concrete_Patch_Payloads(Type t)
		{
			//arrange
			SerializerService serializer = new SerializerService();
			MethodInfo method = serializer.GetType().GetMethod(nameof(serializer.Link));
			MethodInfo genericMethod = method.MakeGenericMethod(t, typeof(PSOBBPatchPacketPayload));

			//assert
			Assert.DoesNotThrow(() => genericMethod.Invoke(serializer, new object[0]));
			serializer.Compile();

			Assert.True(serializer.isTypeRegistered(t), $"Failed to register Type: {t.Name}");
			Assert.True(serializer.isTypeRegistered(typeof(PSOBBPatchPacketPayload)), $"Base packet type wasn't registered.");
		}

		[Test]
		[TestCaseSource(nameof(PatchPayloadTypes))]
		public static void Test_Payload_Has_Parameterless_Ctor(Type t)
		{
			//assert
			Assert.NotNull(t.GetConstructor(Enumerable.Empty<Type>().ToArray()), $"Type: {t.Name} does not have a required parameterless ctor.");
		}

		[Test]
		[TestCaseSource(nameof(PatchPayloadTypes))]
		public static void Test_Can_Serialize_All_Concrete_Patch_Payloads(Type t)
		{
			//arrange
			SerializerService serializer = new SerializerService();
			MethodInfo linkMethodInfo = serializer.GetType().GetMethod(nameof(serializer.Link));
			MethodInfo linkMethod = linkMethodInfo.MakeGenericMethod(t, typeof(PSOBBPatchPacketPayload));
			linkMethod.Invoke(serializer, new object[0]);
			serializer.Compile();

			object payload = Activator.CreateInstance(t);

			//act
			byte[] bytes = null;

			Assert.DoesNotThrow(() => bytes = serializer.Serialize(payload));

			//assert
			Assert.NotNull(bytes);
			Assert.True(bytes.Length != 0);
		}
	}
}
