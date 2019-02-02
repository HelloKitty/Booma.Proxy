using System;
using System.Collections.Generic;
using System.Text;
using Booma.Proxy;
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
		//This is sorta the new design
		//Create an adapter property that will actually handle the adaptor
		//the responsibility of this class is to expose registeration and to
		//handle the internal complicated parts of exposing it to the editor.
		private UnityTextUITextAdapterImplementation Adapter { get; set; }

		//On awake we should just create the adapter for
		//adaptation forwarding.
		void Awake()
		{
			Adapter = new UnityTextUITextAdapterImplementation(UnityUIObject);
		}

		/// <inheritdoc />
		public string Text
		{
			get => Adapter.Text;
			set => Adapter.Text = value;
		}
	}
}
