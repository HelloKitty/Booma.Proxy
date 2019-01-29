using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

namespace Guardians
{
	/// <summary>
	/// Adapter for <see cref="InputField"/> to <see cref="IUIText"/>
	/// </summary>
	public sealed class UnityInputFieldUITextAdapter : BaseUnityUIAdapter<InputField, IUIText>, IUIText
	{
		/// <inheritdoc />
		public string Text
		{
			get => UnityUIObject.text;
			set => UnityUIObject.text = value;
		}
	}
}
