using System;
using System.Collections.Generic;
using System.Text;

namespace Booma.Proxy
{
	//TODO: Support scene types like Lobby and Game.
	public sealed class PSOBBHandlerAttribute : Glader.Essentials.SceneTypeCreateAttribute
	{
		public PSOBBHandlerAttribute() 
			: base(1)
		{

		}
	}
}
