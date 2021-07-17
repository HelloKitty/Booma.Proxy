using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	public enum FrameItemSubClassType : byte
	{
		Frame = 1,
		Barrier = 2,
		Unit = 3
	}

	//Currently parsed from Teth only
	public enum WeaponItemSubClassType : byte
	{
		Saber = 0x1,
		Handgun = 0x06,
		Cane = 0x0A
	}
}
