using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Booma.Proxy
{
	/// <summary>
	/// See <see cref="IUILabeledButton"/>.
	/// </summary>
	public sealed class UnityLabeledButtonAdapter : BaseTwoComponentUnityUIAdapter<Button, Text, IUILabeledButton>, IUILabeledButton
	{
		private UnityTextUITextAdapterImplementation TextAdapter { get; set; }

		private UnityButtonUIButtonAdapterImplementation ButtonAdapter { get; set; }

		void Awake()
		{
			TextAdapter = new UnityTextUITextAdapterImplementation(UnityUIObject2);
			ButtonAdapter = new UnityButtonUIButtonAdapterImplementation(UnityUIObject);
		}

		/// <inheritdoc />
		public string Text
		{
			get => TextAdapter.Text;
			set => TextAdapter.Text = value;
		}

		/// <inheritdoc />
		public void AddOnClickListener(Action action)
		{
			ButtonAdapter.AddOnClickListener(action);
		}

		/// <inheritdoc />
		public void AddOnClickListenerAsync(Func<Task> action)
		{
			ButtonAdapter.AddOnClickListenerAsync(action);
		}

		/// <inheritdoc />
		public bool IsInteractable
		{
			get => ButtonAdapter.IsInteractable;
			set => ButtonAdapter.IsInteractable = value;
		}
	}
}
