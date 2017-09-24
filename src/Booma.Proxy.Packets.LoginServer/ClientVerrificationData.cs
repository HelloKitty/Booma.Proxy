using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	/// <summary>
	/// Verification/Security information for a client.
	/// </summary>
	[WireDataContract]
	public sealed class ClientVerificationData
	{
		//TODO: How is this determined
		/// <summary>
		/// Hardware information (?)
		/// </summary>
		[WireMember(9)]
		public long HardwareInformation { get; }

		//TODO: What is this really?
		/// <summary>
		/// Client security information (?)
		/// </summary>
		[KnownSize(40)]
		[WireMember(10)]
		public byte[] SecurityData { get; }

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
