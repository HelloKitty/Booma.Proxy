using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guardians;
using UnityEngine;

namespace Booma.Proxy
{
	public abstract class BaseTwoComponentUnityUIAdapter<TAdaptedUnityEngineType, TAdaptedUnityEngineType2, TAdaptedToType> : BaseUnityUIAdapter<TAdaptedUnityEngineType, TAdaptedToType>
	{
		[SerializeField]
		protected TAdaptedUnityEngineType2 _UnityUIObject2;

		/// <summary>
		/// The Unity engine UI object being adapted.
		/// </summary>
		protected TAdaptedUnityEngineType2 UnityUIObject2 => _UnityUIObject2;
	}
}
