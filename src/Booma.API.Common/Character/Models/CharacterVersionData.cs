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
		internal byte V2Flags { get; set; }

		//TODO: What is this?
		[WireMember(2)]
		internal byte Version { get; set; }

		//TODO: What is this?
		[WireMember(3)]
		internal uint V1Flags { get; set; }

		//Serializer ctor.
		private CharacterVersionData()
		{
			
		}
	}
}
