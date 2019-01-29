using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Guardians
{
	//Used for revalidation
	[ExecuteInEditMode]
	public sealed class UnityImageUIFillableImageAdapter : BaseUnityUIImageAdapter<IUIFillableImage>, IUIFillableImage
	{
		//TODO: This won't hold up if the Type changes.
		//Override validation to check image is fillable
		protected override bool ValidateInitializedObject(Image obj)
		{
			bool result = base.ValidateInitializedObject(obj);

			if(!result)
				return false;

			//Else, if it's valid for the base we need to check if it's fillable
			//image otherwise it won't work as a fillable image
			if(obj.type != Image.Type.Filled)
			{
				UnityEngine.Debug.LogError($"Failed to initialize Image on GameObject: {obj.gameObject.name} as Fillable Image. Was ImageType: {obj.type}");
				return false;
			}

			return true;
		}

		private void Update()
		{
			//Just call to revalidation
			ValidateInitializedObject(UnityUIObject);
		}

		/// <inheritdoc />
		public float FillAmount
		{
			get => UnityUIObject.fillAmount;
			set => UnityUIObject.fillAmount = Mathf.Clamp(value, 0, 1.0f);
		}
	}
}
