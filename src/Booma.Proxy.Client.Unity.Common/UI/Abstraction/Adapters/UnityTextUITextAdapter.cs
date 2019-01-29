using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Guardians
{
	/// <summary>
	/// Adapters for the Unity3D UI Type: <see cref="Text"/> adapted to
	/// Guardians Type: <see cref="IUIText"/>.
	/// </summary>
	public sealed class UnityTextUITextAdapter : BaseUnityUIAdapter<Text, IUIText>, IUIText
	{
		/// <inheritdoc />
		public string Text
		{
			get => UnityUIObject.text;
			set => UnityUIObject.text = value;
		}
	}
}
