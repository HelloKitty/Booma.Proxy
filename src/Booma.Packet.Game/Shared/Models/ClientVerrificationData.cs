using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;
using Reinterpret.Net;

namespace Booma.Proxy
{
	/// <summary>
	/// Verification/Security information for a client.
	/// </summary>
	[WireDataContract]
	public sealed class ClientVerificationData
	{
		public static ClientVerificationData FromVersionString(string versionString)
		{
			byte[] bytes = versionString.Reinterpret(Encoding.ASCII);
			return new ClientVerificationData(0x41, bytes.Concat(Enumerable.Repeat((byte)0, 40 - bytes.Length)).ToArray());
		}

		//TODO: How is this determined
		/// <summary>
		/// Hardware information (?)
		/// </summary>
		[WireMember(9)]
		public long HardwareInformation { get; internal set; }

		//TODO: What is this really?
		/// <summary>
		/// Client security information (?)
		/// </summary>
		[KnownSize(40)]
		[WireMember(10)]
		public byte[] SecurityData { get; internal set; }

		public ClientVerificationData(long hardwareInformation, [NotNull] byte[] securityData)
		{
			if(securityData == null) throw new ArgumentNullException(nameof(securityData));
			if(securityData.Length > 40) throw new ArgumentException($"The {nameof(securityData)} was greater than the KnownSize attribute.");

			HardwareInformation = hardwareInformation;
			SecurityData = securityData;
		}

		//Serializer ctor
		private ClientVerificationData()
		{
			
		}
	}
}
