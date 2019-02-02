using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Provider the ability for ordered collections of element Type <typeparamref name="TAdaptedToType"/>
	/// to be adapted and registered together.
	/// </summary>
	public abstract class BaseCollectionUnityUIAdapter<TAdaptedToType> : BaseUnityUI<IReadOnlyCollection<TAdaptedToType>>, IReadOnlyCollection<TAdaptedToType>
	{
		[OdinSerialize] //probably required due to the generic-ness.
		[Tooltip("Order matters. This is the collection of elements to be exposed.")]
		private TAdaptedToType[] Elements;

		[Button]
		public void ValidateComponent()
		{
			if(Elements == null)
				Elements = new TAdaptedToType[0];

			Elements = Elements.Where(e => e != null).ToArray();
		}

		/// <inheritdoc />
		public IEnumerator<TAdaptedToType> GetEnumerator()
		{
			return ((IEnumerable<TAdaptedToType>)Elements).GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Elements.GetEnumerator();
		}

		/// <inheritdoc />
		public int Count => Elements.Length;
	}
}
