using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using NUnit.Framework;

namespace Booma.Proxy
{
	[TestFixture]
	public class Subcommand60Tests
	{
		[Test]
		public void Test_Can_Serialize_Subcommand60Payload()
		{
			//arrange
			SerializerService serializer = new SerializerService();
			serializer.RegisterType<Sub60MovingFastPositionSetCommand>();
			serializer.Compile();
			
			//act
			byte[] bytes = serializer.Serialize(new Sub60MovingFastPositionSetCommand(5, new Vector2<float>(2, 5)));

			//assert
			Assert.NotNull(bytes);
			Assert.True(bytes.Length != 0);
		}

		[Test]
		public void Test_Can_Serialize_Then_Deserialize_Subcommand60Payload()
		{
			//arrange
			SerializerService serializer = new SerializerService();
			serializer.RegisterType<Sub60MovingFastPositionSetCommand>();
			serializer.Compile();

			//act
			byte[] bytes = serializer.Serialize(new Sub60MovingFastPositionSetCommand(5, new Vector2<float>(2, 5)));
			Sub60MovingFastPositionSetCommand desserialized = serializer.Deserialize<BaseSubCommand60>(bytes)
				as Sub60MovingFastPositionSetCommand;

			//assert
			Assert.NotNull(desserialized, "Object was null.");
			Assert.NotNull(desserialized.Position);
			Assert.AreEqual(5, desserialized.Identifier);
			Assert.True(Math.Abs(desserialized.Position.X - 2) < float.Epsilon);
		}
	}
}
