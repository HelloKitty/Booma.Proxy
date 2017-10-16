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
		private byte _slotSelected;

		/// <inheritdoc />
		public byte SlotSelected
		{
			get => _slotSelected;
			set
			{
				_slotSelected = value; 
				SaveSlotSelected();
				
			}
		}

		private void Awake()
		{
			//TODO: Validate
			_slotSelected = (byte)PlayerPrefs.GetInt(nameof(SlotSelected), -1);
		}

		private void SaveSlotSelected()
		{
			PlayerPrefs.SetInt(nameof(SlotSelected), _slotSelected);
		}
	}
}
