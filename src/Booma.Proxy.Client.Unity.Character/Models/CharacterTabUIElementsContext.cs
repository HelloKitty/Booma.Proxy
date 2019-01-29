using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Autofac.Features.AttributeFilters;
using Guardians;
using UnityEngine;
using UnityEngine.UI;

namespace Booma.Proxy
{
	public sealed class CharacterTabUIElementsContext
	{
		public sealed class CharacterTabUIElement
		{
			public IUIButton ButtonElement { get; }

			public IUIText TextElement { get; }

			/// <inheritdoc />
			public CharacterTabUIElement([NotNull] IUIButton buttonElement, [NotNull] IUIText textElement)
			{
				ButtonElement = buttonElement ?? throw new ArgumentNullException(nameof(buttonElement));
				TextElement = textElement ?? throw new ArgumentNullException(nameof(textElement));
			}
		}

		public IReadOnlyCollection<CharacterTabUIElement> Elements { get; }

		//This ctor looks BRUTAL but it's auto-wired with elements
		//from the scene via IoC. And external consumers access through the Elements prop
		//so it appears slightly cleanish.
		public CharacterTabUIElementsContext(
			[KeyFilter(UnityUIRegisterationKey.CharacterSlot1)] IUIButton button1,
			[KeyFilter(UnityUIRegisterationKey.CharacterSlot2)] IUIButton button2,
			[KeyFilter(UnityUIRegisterationKey.CharacterSlot3)] IUIButton button3,
			[KeyFilter(UnityUIRegisterationKey.CharacterSlot4)] IUIButton button4,
			[KeyFilter(UnityUIRegisterationKey.CharacterSlot1)] IUIText text1,
			[KeyFilter(UnityUIRegisterationKey.CharacterSlot2)] IUIText text2,
			[KeyFilter(UnityUIRegisterationKey.CharacterSlot3)] IUIText text3,
			[KeyFilter(UnityUIRegisterationKey.CharacterSlot4)] IUIText text4)
		{
			CharacterTabUIElement[] characterTabElements = new CharacterTabUIElement[4];

			characterTabElements[0] = new CharacterTabUIElement(button1, text1);
			characterTabElements[1] = new CharacterTabUIElement(button2, text2);
			characterTabElements[2] = new CharacterTabUIElement(button3, text3);
			characterTabElements[3] = new CharacterTabUIElement(button4, text4);

			Elements = characterTabElements;
		}
	}
}
