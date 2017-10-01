using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	//Syl: LOGIN_93_TYPE https://github.com/Sylverant/login_server/blob/master/src/bblogin.c#L121
	//Teth: https://github.com/justnoxx/psobb-tethealla/blob/master/login_server/login_server.c#L4537 I think?
	[WireDataContract]
	[LoginClientPacketPayload(LoginNetworkOperationCode.LOGIN_93_TYPE)]
	public sealed class LoginLoginRequest93Payload : PSOBBLoginPacketPayloadClient
	{
		//TODO: What is this?
		[WireMember(1)]
		private int Tag { get; }

		/// <summary>
		/// Unused
		/// </summary>
		[WireMember(2)]
		private uint GuildCardId { get; }

		/// <summary>
		/// Client version moniker.
		/// </summary>
		[WireMember(3)]
		public ushort ClientVersion { get; }

		/// <summary>
		/// Unknown bytes.
		/// </summary>
		[KnownSize(6)]
		[WireMember(4)]
		private byte[] unk2 { get; } = new byte[6];

		/// <summary>
		/// Unused
		/// </summary>
		[WireMember(5)]
		private uint TeamId { get; }

		/// <summary>
		/// The username to authenticate with.
		/// </summary>
		[KnownSize(16)] //username can be 15 char ASCII string with a null terminator
		[WireMember(6)]
		public string UserName { get; }

		/// <summary>
		/// Unused
		/// </summary>
		[KnownSize(32)]
		[WireMember(7)]
		private byte[] unused1 { get; } = new byte[32];

		/// <summary>
		/// The password to authenticate with.
		/// </summary>
		[KnownSize(16)] //password can be 15 char ASCII string with a null terminator
		[WireMember(8)]
		public string Password { get; }

		/// <summary>
		/// Unusued
		/// </summary>
		[KnownSize(40)]
		[WireMember(9)]
		private byte[] unused2 { get; } = new byte[40];

		/// <summary>
		/// Verification/security information the client is using for the session.
		/// </summary>
		[WireMember(10)]
		public ClientVerificationData ClientData { get; }

		public LoginLoginRequest93Payload(ushort clientVersion, [NotNull] string userName, [NotNull] string password, [NotNull] ClientVerificationData clientData)
		{
			if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));
			if(string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));
			if(userName.Length > 15) throw new ArgumentException($"{nameof(userName)} had a length of {userName.Length} but maximum length supported is 15.", nameof(userName));
			if(password.Length > 15) throw new ArgumentException($"{nameof(password)} had a length of {password.Length} but maximum length supported is 15.", nameof(userName));
			if(clientVersion == 0) throw new ArgumentOutOfRangeException(nameof(clientVersion));
			if(clientData == null) throw new ArgumentNullException(nameof(clientData));

			ClientVersion = clientVersion;
			UserName = userName;
			Password = password;
			ClientData = clientData;
		}

		//Serializer ctor
		protected LoginLoginRequest93Payload()
		{

		}

		#region UNUSED/UNKNOWN


		#endregion
	}
}
