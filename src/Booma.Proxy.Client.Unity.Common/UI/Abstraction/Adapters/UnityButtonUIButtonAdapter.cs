using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy;
using UnityEngine.UI;
using Unitysync.Async;

namespace Guardians
{
	public sealed class UnityButtonUIButtonAdapter : BaseUnityUIAdapter<Button, IUIButton>, IUIButton
	{
		//This is sorta the new design
		//Create an adapter property that will actually handle the adaptor
		//the responsibility of this class is to expose registeration and to
		//handle the internal complicated parts of exposing it to the editor.
		private UnityButtonUIButtonAdapterImplementation Adapter { get; set; }

		//On awake we should just create the adapter for
		//adaptation forwarding.
		void Awake()
		{
			Adapter = new UnityButtonUIButtonAdapterImplementation(UnityUIObject);
		}

		/// <inheritdoc />
		public void AddOnClickListener(Action action)
		{
			Adapter.AddOnClickListener(action);
		}

		/// <inheritdoc />
		public void AddOnClickListenerAsync(Func<Task> action)
		{
			Adapter.AddOnClickListenerAsync(action);
		}

		/// <inheritdoc />
		public bool IsInteractable
		{
			get => Adapter.IsInteractable;
			set => Adapter.IsInteractable = value;
		}
	}
}
