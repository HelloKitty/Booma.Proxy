using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	/// <summary>
	/// Contract for any model that provides settings/config/data
	/// for a zone.
	/// </summary>
	public interface IZoneSettings
	{
		/// <summary>
		/// The ID for the current zone.
		/// </summary>
		short ZoneId { get; }
	}
}
