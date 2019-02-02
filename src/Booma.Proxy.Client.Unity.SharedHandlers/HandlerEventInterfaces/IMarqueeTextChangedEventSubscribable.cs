using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public interface IMarqueeTextChangedEventSubscribable
	{
		event EventHandler<MarqueeTextChangedEventArgs> OnMarqueeTextChangedEvent;
	}

	//You might wonder why we don't just send a string
	//My concern is that there is more data in the packet
	//so we may have to send that too at some point.
	public sealed class MarqueeTextChangedEventArgs : EventArgs
	{
		public string ChangedText { get; }

		/// <inheritdoc />
		public MarqueeTextChangedEventArgs([NotNull] string changedText)
		{
			//Could potentially be empty? Let's not assume
			ChangedText = changedText ?? throw new ArgumentNullException(nameof(changedText));
		}
	}
}
