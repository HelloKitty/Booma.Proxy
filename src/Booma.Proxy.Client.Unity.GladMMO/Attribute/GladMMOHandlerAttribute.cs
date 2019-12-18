using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.Proxy
{
	//TODO: Support scene types like Lobby and Game.
	public sealed class GladMMOHandlerAttribute : Glader.Essentials.SceneTypeCreateAttribute
	{
		public GladMMOHandlerAttribute() 
			: base(1)
		{

		}
	}
}
