using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	/// <summary>
	/// Enumeration of customizable slots for PSOBB characters.
	/// </summary>
	public enum PsobbCustomizationSlots
	{
		/// <summary>
		/// The costume customizable slot of the PSOBB character.
		/// </summary>
		Costume = 1,

		/// <summary>
		/// The skin type of the PSOBB character.
		/// </summary>
		Skin = 2,

		/// <summary>
		/// The face slot of the PSOBB character.
		/// </summary>
		Face = 3,

		/// <summary>
		/// The head slot of the PSOBB character.
		/// </summary>
		Head = 4,

		/// <summary>
		/// The hair slot of the PSOBB character.
		/// </summary>
		Hair = 5,

		/// <summary>
		/// The model override of the PSOBB character.
		/// (Ex. Flowen, Sonic, Rico)
		/// </summary>
		Override = 6,
	}

	/// <summary>
	/// Enumeration of all PSOBB proportion slots.
	/// </summary>
	public enum PsobbProportionSlots
	{
		/// <summary>
		/// The default and only proportion slot in PSO.
		/// </summary>
		Default = 1,
	}
}
