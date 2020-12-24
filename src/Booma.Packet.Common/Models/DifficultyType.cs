using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Enumeration of all the difficulty types.
	/// </summary>
	public enum DifficultyType : byte
	{
		//TODO: Are these values right?
		Normal = 0,
		Hard = 1,
		VeryHard = 2,
		Ultimate = 3,
	}
}
