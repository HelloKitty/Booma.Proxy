using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IBlockListingEventSubscribable
	{
		/// <summary>
		/// Event fired when a block listing is recieved.
		/// </summary>
		event EventHandler<BlockListingDataRecievedEventArgs> OnBlockListingRecieved;

		/// <summary>
		/// Event that should be fired when a block list has been fully recieved.
		/// </summary>
		event EventHandler OnBlockListFinishedRecieving;
	}

	public sealed class BlockListingDataRecievedEventArgs
	{
		/// <summary>
		/// The identifier for the menu option.
		/// </summary>
		public MenuItemIdentifier Identifier { get; }

		public string BlockName { get; }

		/// <inheritdoc />
		public BlockListingDataRecievedEventArgs([NotNull] MenuItemIdentifier identifier, [NotNull] string blockName)
		{
			Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
			BlockName = blockName ?? throw new ArgumentNullException(nameof(blockName));
		}
	}
}
