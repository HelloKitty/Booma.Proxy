﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using NUnit.Framework;

namespace Booma
{
	[TestFixture]
	public class Subcommand60Tests
	{
		[Test]
		public void Test_Can_Serialize_Subcommand60Payload()
		{
			//arrange
			SerializerService serializer = new SerializerService();
			serializer.RegisterPolymorphicSerializer<BaseSubCommand60, BaseSubCommand60_Serializer>();
			Span<byte> buffer = new Span<byte>(new byte[1000]);
			int offset = 0;

			//act
			serializer.Write(new Sub60MovingFastPositionSetCommand(5, new Vector2<float>(2, 5)), buffer, ref offset);

			//assert
			Assert.True(offset != 0);
		}

		[Test]
		public void Test_Can_Serialize_Then_Deserialize_Subcommand60Payload()
		{
			//arrange
			SerializerService serializer = new SerializerService();
			serializer.RegisterPolymorphicSerializer<BaseSubCommand60, BaseSubCommand60_Serializer>();
			Span<byte> buffer = new Span<byte>(new byte[1000]);
			int offset = 0;

			//act
			serializer.Write(new Sub60MovingFastPositionSetCommand(5, new Vector2<float>(2, 5)), buffer, ref offset);
			Sub60MovingFastPositionSetCommand desserialized = serializer.Read<BaseSubCommand60>(buffer.Slice(0, offset), 0)
				as Sub60MovingFastPositionSetCommand;

			//assert
			Assert.NotNull(desserialized, "Object was null.");
			Assert.NotNull(desserialized.Position);
			Assert.AreEqual(5, desserialized.Identifier);
			Assert.True(Math.Abs(desserialized.Position.X - 2) < float.Epsilon);
		}
	}
}
