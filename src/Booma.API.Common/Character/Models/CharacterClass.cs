﻿namespace Booma
{
	/// <summary>
	/// PSO doesn't have races and classes. Just a combination of both.
	/// Unlike WoW where you can have a Dwarf Warlock there is only something akin to a DWLock.
	/// </summary>
	public enum CharacterClass : byte
	{
		HUmar,
		HUnewearl,
		HUcast,
		RAmar,
		RAcast,
		RAcaseal,
		FOmarl,
		FOnewm,
		FOnewearl,
		HUcaseal,
		FOmar,
		RAmarl,

		//TODO: move these somewhere for use in sectionID calculation
		/*HUmar = 0,

		HUnewearl = 1,

		HUcast = 2,

		HUcaseal = 3,

		RAmar = 4,

		RAmarl = 5,

		RAcast = 6,

		RAcaseal = 7,

		FOmar = 8,

		FOmarl = 9,

		FOnewm = 10,

		FOnewearl = 11*/
	}
}