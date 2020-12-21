using FreecraftCore.Serializer;

namespace Booma.Proxy
{
	/// <summary>
	/// Currently unknown version data for the character.
	/// </summary>
	[WireDataContract]
	public sealed class CharacterVersionData
	{
		//TODO: What is this?
		[WireMember(1)]
		private byte V2Flags { get; }

		//TODO: What is this?
		[WireMember(2)]
		private byte Version { get; }

		//TODO: What is this?
		[WireMember(3)]
		private uint V1Flags { get; }

		//Serializer ctor.
		private CharacterVersionData()
		{
			
		}
	}
}