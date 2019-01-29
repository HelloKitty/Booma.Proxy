using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Guardians
{
	public abstract class BaseUnityUIImageAdapter<TAdaptedToType> : BaseUnityUIAdapter<Image, TAdaptedToType>, IUIImage
		where TAdaptedToType : IUIImage //just make sure it's a IUIImage
	{
		/// <inheritdoc />
		public void SetSpriteTexture(Texture2D texture)
		{
			if(UnityUIObject.sprite == null)
				UnityUIObject.sprite = Sprite.Create(texture, Rect.zero, Vector2.zero); //TODO: What should defaults be?
			else
			{
				//Sprites complain if we don't have proper size, so we need size based on the texture2D
				UnityUIObject.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
			}
		}
	}
}
