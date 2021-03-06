using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore.Serializer;
using JetBrains.Annotations;

namespace Booma
{
	//Syl: LOGIN_93_TYPE https://github.com/Sylverant/login_server/blob/master/src/bblogin.c#L121
	//Teth: https://github.com/justnoxx/psobb-tethealla/blob/master/login_server/login_server.c#L4537 I think?
	[WireDataContract]
	[GameClientPacketPayload(GameNetworkOperationCode.LOGIN_93_TYPE)]
	public sealed partial class SharedLoginRequest93Payload : PSOBBGamePacketPayloadClient
	{
		//TODO: This may not be serverType. It may have to do with the current operation
		[Serializable]
		public enum SessionStage : byte
		{
			/// <summary>
			/// Session for pre-character selection.
			/// </summary>
			BeforeCharacterSelection = 0,

			/// <summary>
			/// Session for being at the character selection screen.
			/// </summary>
			CharacterSelection = 1,

			/// <summary>
			/// The ID for the pre-ship auth for <see cref="SharedLoginRequest93Payload.unk2"/>.
			/// </summary>
			PreShip = 4,

			/// <summary>
			/// The ID for the ship flow <see cref="SharedLoginRequest93Payload.unk2"/>
			/// </summary>
			Ship = 5,
		}

		//TODO: What is this?
		[WireMember(1)]
		internal int Tag { get; set; }

		/// <summary>
		/// Unused
		/// </summary>
		[WireMember(2)]
		public uint GuildCardId { get; internal set; }

		/// <summary>
		/// Client version moniker.
		/// </summary>
		[WireMember(3)]
		public ushort ClientVersion { get; internal set; }

		//Soly said: meet the user will put a 6 here and teth puts the lobby you want to go, in offset 0x7C
		/// <summary>
		/// Unknown bytes.
		/// </summary>
		[KnownSize(6)]
		[WireMember(4)]
		internal byte[] unk2 { get; set; } //Tethella will expect a 4 at 0x16 during Character and 5 during Ship.

		//TODO: Some work needs to be done to create a proper representative structure for unk2.
		/// <summary>
		/// Indicates the stage the session trying to login in is at.
		/// </summary>
		public SessionStage Stage => (SessionStage)unk2[4];

		/// <summary>
		/// Indicates the stage the session trying to login in is at.
		/// </summary>
		public byte SelectedSlot => unk2[3];

		//easier to work with this as an int in .NET/Unity3D
		/// <summary>
		/// Unused
		/// </summary>
		[WireMember(5)]
		internal int TeamId { get; set; }

		//This is really 16 characters. 15 char + 1 terminator.
		/// <summary>
		/// The username to authenticate with.
		/// </summary>
		[KnownSize(15)] //username can be 15 char ASCII string with a null terminator
		[WireMember(6)]
		public string UserName { get; internal set; }

		/// <summary>
		/// Unused
		/// </summary>
		[KnownSize(32)]
		[WireMember(7)]
		internal byte[] unused1 { get; set; } = Array.Empty<byte>();

		//This is really 16 characters. 15 char + 1 terminator.
		/// <summary>
		/// The password to authenticate with.
		/// </summary>
		[KnownSize(15)] //password can be 15 char ASCII string with a null terminator
		[WireMember(8)]
		public string Password { get; internal set; }

		/// <summary>
		/// Unusued
		/// </summary>
		[KnownSize(40)]
		[WireMember(9)]
		internal byte[] unused2 { get; set; } = Array.Empty<byte>();

		/// <summary>
		/// Verification/security information the client is using for the session.
		/// </summary>
		[WireMember(10)]
		public ClientVerificationData ClientData { get; internal set; }

		public SharedLoginRequest93Payload(ushort clientVersion, [NotNull] string userName, [NotNull] string password, [NotNull] ClientVerificationData clientData, SessionStage stage = SessionStage.PreShip)
			: this(clientVersion, 0, userName, password, clientData, stage)
		{

		}

		public SharedLoginRequest93Payload(ushort clientVersion, int teamId, [NotNull] string userName, [NotNull] string password, [NotNull] ClientVerificationData clientData, SessionStage stage = SessionStage.PreShip)
			: this(clientVersion, teamId, 0, userName, password, clientData, stage)
		{

		}

		public SharedLoginRequest93Payload(ushort clientVersion, int teamId, uint guildCard, [NotNull] string userName, [NotNull] string password, [NotNull] ClientVerificationData clientData, SessionStage stage = SessionStage.PreShip)
			: this()
		{
			if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));
			if(string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));
			if(userName.Length > 15) throw new ArgumentException($"{nameof(userName)} had a length of {userName.Length} but maximum length supported is 15.", nameof(userName));
			if(password.Length > 15) throw new ArgumentException($"{nameof(password)} had a length of {password.Length} but maximum length supported is 15.", nameof(userName));
			if(clientVersion == 0) throw new ArgumentOutOfRangeException(nameof(clientVersion));
			if(clientData == null) throw new ArgumentNullException(nameof(clientData));

			ClientVersion = clientVersion;
			TeamId = teamId;
			UserName = userName;
			Password = password;
			ClientData = clientData;
			GuildCardId = guildCard;

			//This is odd, not sure what this is or why we have to do it but Teth checks this sometimes
			unk2 = Enumerable.Repeat((byte)stage, 6).ToArray();
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public SharedLoginRequest93Payload()
			: base(GameNetworkOperationCode.LOGIN_93_TYPE)
		{

		}
	}
}
