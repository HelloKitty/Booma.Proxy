using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma.Proxy
{
	public static class SubCommand60Extensions
	{
		/// <summary>
		/// Creates a new <see cref="BlockNetworkCommandEventClientPayload"/> with the provided <see cref="command"/> 0x60
		/// command.
		/// </summary>
		/// <param name="command">The command to initialize the payload with.</param>
		/// <returns>A new <see cref="BlockNetworkCommandEventClientPayload"/> with the <see cref="command"/></returns>
		public static BlockNetworkCommandEventClientPayload ToPayload([NotNull] this BaseSubCommand60 command)
		{
			if(command == null) throw new ArgumentNullException(nameof(command));
			
			//Just create a new command container (the 0x60 payload) around the command.
			return new BlockNetworkCommandEventClientPayload(command);
		}
	}
}
