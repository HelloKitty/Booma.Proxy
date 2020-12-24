using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface ISubCommand<out TCommandOperationCode>
		where TCommandOperationCode : Enum
	{
		/// <summary>
		/// The operation code for the subcommand.
		/// This is only read for logging of unknown subcommands.
		/// </summary>
		TCommandOperationCode CommandOperationCode { get; }
	}

	public interface ISubCommand
	{
		/// <summary>
		/// Indicates if the <see cref="CommandSize"/> property is serialized and
		/// deserialized.
		/// Child Types can override this to gain access to the single Byte size if needed.
		/// </summary>
		bool isSizeSerialized { get; }

		//Since the Type byte is eaten by the polymorphic deserialization process
		/// <summary>
		/// The size of the subcommand (subpayload).
		/// Not needed for deserialization of subcommand.
		/// </summary>
		byte CommandSize { get; }
	}
}
