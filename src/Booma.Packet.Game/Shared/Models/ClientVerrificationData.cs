using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;
using Reinterpret.Net;

namespace Booma
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
		public long HardwareInformation { get; internal set; }

		//TODO: What is this really?
		/// <summary>
		/// Client security information (?)
		/// </summary>
		[KnownSize(40)]
		[WireMember(10)]
		public SecurityData SecurityData { get; internal set; }

		public ClientVerificationData(long hardwareInformation, SecurityData securityData)
			: this()
		{
			HardwareInformation = hardwareInformation;
			SecurityData = securityData ?? throw new ArgumentNullException(nameof(securityData));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public ClientVerificationData()
		{
			
		}
	}
}
