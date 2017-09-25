using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Booma.Proxy.Packets
{
	[TestFixture]
	public class ConnectionRedirectPayloadTests
	{
		[Test]
		[TestCase((short)55)]
		[TestCase((short)0)]
		[TestCase((short)short.MaxValue)]
		[TestCase((short)short.MinValue)]
		public void Test_Can_Serialize_Port_Only_Redirect(short port)
		{
			//arrange
			SerializerService serializer = new SerializerService();
			serializer.Link<ConnectionRedirectPayload, PSOBBShipPacketPayload>();
			serializer.Compile();
			ConnectionRedirectPayload payload = new ConnectionRedirectPayload(port);

			//act
			byte[] bytes = serializer.Serialize(payload);

			//assert
			Assert.NotNull(bytes);
			Assert.IsNotEmpty(bytes);
			Assert.True(bytes.Length == 8, $"Size was {bytes.Length}");
			Assert.AreEqual(port, payload.EndpointPort);
		}

		[Test]
		[TestCase((short)55)]
		[TestCase((short)0)]
		[TestCase((short)short.MaxValue)]
		[TestCase((short)short.MinValue)]
		public void Test_Can_Deserialize_Port_Only_Redirect(short port)
		{
			//arrange
			SerializerService serializer = new SerializerService();
			serializer.Link<ConnectionRedirectPayload, PSOBBShipPacketPayload>();
			serializer.Compile();
			ConnectionRedirectPayload payload = new ConnectionRedirectPayload(port);

			//act
			byte[] bytes = serializer.Serialize(payload);
			ConnectionRedirectPayload deserializedPayload = serializer.Deserialize<ConnectionRedirectPayload>(bytes);

			//assert
			Assert.AreEqual(port, deserializedPayload.EndpointPort);
			Assert.AreEqual(payload.EndpointPort, deserializedPayload.EndpointPort);
			Assert.False(payload.isNewIpAddressRedirect);
			Assert.False(deserializedPayload.isNewIpAddressRedirect);
		}

		[Test]
		[TestCase((short)55, "127.0.0.1")]
		[TestCase((short)0, "198.1.3.45")]
		[TestCase((short)short.MaxValue, "69.0.0.99")]
		[TestCase((short)short.MinValue, "127.1.0.1")]
		public void Test_Can_Serialize_Ip_And_Port_Redirect(short port, string ip)
		{
			//arrange
			SerializerService serializer = new SerializerService();
			serializer.Link<ConnectionRedirectPayload, PSOBBShipPacketPayload>();
			serializer.Compile();
			ConnectionRedirectPayload payload = new ConnectionRedirectPayload(ip, port);

			//act
			byte[] bytes = serializer.Serialize(payload);

			//assert
			Assert.NotNull(bytes);
			Assert.IsNotEmpty(bytes);
			Assert.AreEqual(port, payload.EndpointPort);
		}


		[Test]
		[TestCase((short)55, "127.0.0.1")]
		[TestCase((short)0, "198.1.3.45")]
		[TestCase((short)short.MaxValue, "69.0.0.99")]
		[TestCase((short)short.MinValue, "127.1.0.1")]
		public void Test_Can_Deserialize_Ip_And_Port_Redirect(short port, string ip)
		{
			//arrange
			SerializerService serializer = new SerializerService();
			serializer.Link<ConnectionRedirectPayload, PSOBBShipPacketPayload>();
			serializer.Compile();
			ConnectionRedirectPayload payload = new ConnectionRedirectPayload(ip, port);

			//act
			byte[] bytes = serializer.Serialize(payload);
			ConnectionRedirectPayload deserializedPayload = serializer.Deserialize<ConnectionRedirectPayload>(bytes);
			byte[] bytesTwo = serializer.Serialize(deserializedPayload);

			//assert
			Assert.AreEqual(port, deserializedPayload.EndpointPort);
			Assert.AreEqual(payload.EndpointPort, deserializedPayload.EndpointPort);
			Assert.True(payload.isNewIpAddressRedirect);
			Assert.True(deserializedPayload.isNewIpAddressRedirect);

			Assert.NotNull(deserializedPayload.EndpointAddress);
			Assert.AreEqual(ip, deserializedPayload.EndpointAddress.ToString());
			Assert.AreEqual(ip, payload.EndpointAddress.ToString());

			for(int i = 0; i < bytes.Length || i < bytesTwo.Length; i++)
				Assert.AreEqual(bytes[i], bytesTwo[i]);
		}
	}
}
