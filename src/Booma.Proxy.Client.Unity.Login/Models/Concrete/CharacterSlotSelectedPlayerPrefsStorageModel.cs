using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Simple model that loads/saves the character slot selected
	/// in <see cref="PlayerPrefs"/>.
	/// </summary>
	public sealed class CharacterSlotSelectedPlayerPrefsStorageModel : MonoBehaviour, ICharacterSlotSelectedModel
	{
		private int _slotSelected;

		/// <inheritdoc />
		public int SlotSelected
		{
			get => _slotSelected;
			set { _slotSelected = value; }
		}

		private void Awake()
		{
			_slotSelected = PlayerPrefs.GetInt(nameof(SlotSelected), -1);
		}

		private void SaveSlotSelected()
		{
			PlayerPrefs.SetInt(nameof(SlotSelected), _slotSelected);
		}
	}
}
