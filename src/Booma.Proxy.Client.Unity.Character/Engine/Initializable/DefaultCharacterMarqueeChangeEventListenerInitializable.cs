using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Features.AttributeFilters;
using Guardians;

namespace Booma.Proxy
{
	//You might ask What marquee on Character??
	//Well, Character port services part of the server selection screen such
	//as the marquee.
	[SceneTypeCreate(GameSceneType.ServerSelectionScreen)]
	public sealed class DefaultCharacterMarqueeChangeEventListenerInitializable : BaseSingleEventListenerInitializable<IMarqueeTextChangedEventSubscribable, MarqueeTextChangedEventArgs>
	{
		private IUIText MarqueeTextElement { get; }

		/// <inheritdoc />
		public DefaultCharacterMarqueeChangeEventListenerInitializable(IMarqueeTextChangedEventSubscribable subscriptionService,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.Marquee)] IUIText marqueeTextElement)
			: base(subscriptionService)
		{
			MarqueeTextElement = marqueeTextElement ?? throw new ArgumentNullException(nameof(marqueeTextElement));
		}

		/// <inheritdoc />
		protected override void OnEventFired(object source, MarqueeTextChangedEventArgs args)
		{
			//handle event here!
			//Assuming we're on the main thread, we can just set this
			MarqueeTextElement.Text = args.ChangedText;
		}
	}
}
