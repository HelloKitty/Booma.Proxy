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
		/// Creates a new <see cref="BlockNetworkCommand60EventClientPayload"/> with the provided <see cref="command"/> 0x60
		/// command.
		/// </summary>
		/// <param name="command">The command to initialize the payload with.</param>
		/// <returns>A new <see cref="BlockNetworkCommand60EventClientPayload"/> with the <see cref="command"/></returns>
		public static BlockNetworkCommand60EventClientPayload ToPayload([NotNull] this BaseSubCommand60 command)
		{
			if(command == null) throw new ArgumentNullException(nameof(command));
			
			//Just create a new command container (the 0x60 payload) around the command.
			return new BlockNetworkCommand60EventClientPayload(command);
		}

		/// <summary>
		/// Creates a new <see cref="BlockNetworkCommand62EventClientPayload"/> with the provided <see cref="command"/> 0x60
		/// command.
		/// </summary>
		/// <param name="command">The command to initialize the payload with.</param>
		/// <returns>A new <see cref="BlockNetworkCommand62EventClientPayload"/> with the <see cref="command"/></returns>
		public static BlockNetworkCommand62EventClientPayload ToPayload([NotNull] this BaseSubCommand62 command)
		{
			if(command == null) throw new ArgumentNullException(nameof(command));

			//Just create a new command container (the 0x60 payload) around the command.
			return new BlockNetworkCommand62EventClientPayload(command);
		}

		//TODO: We can probably cache this string for performance in the future
		/// <summary>
		/// Computes the hex string opcode for the provided <see cref="BaseSubCommand60"/>.
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public static string OpCodeHexString(this BaseSubCommand60 command)
		{
			return $"0x60 {command.CommandOperationCode:X}";
		}

		//TODO: We can probably cache this string for performance in the future
		/// <summary>
		/// Computes the hex string opcode for the provided <see cref="BaseSubCommand62"/>.
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		public static string OpCodeHexString(this BaseSubCommand62 command)
		{
			return $"0x62 {command.CommandOperationCode:X}";
		}
	}
}
