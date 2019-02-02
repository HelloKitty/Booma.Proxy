using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class DefaultCharacterMarqueeChangeEventListenerInitializable : BaseSingleEventListenerInitializable<IMarqueeTextChangedEventSubscribable, MarqueeTextChangedEventArgs>
	{
		/// <inheritdoc />
		public DefaultCharacterMarqueeChangeEventListenerInitializable(IMarqueeTextChangedEventSubscribable subscriptionService) 
			: base(subscriptionService)
		{

		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, MarqueeTextChangedEventArgs args)
		{
			//handle event here!
		}
	}
}
