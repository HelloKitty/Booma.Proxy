using System;
using System.Collections.Generic;
using System.Text;

namespace Booma
{
	//TODO: Move this to Glader.Essentials.
	public static class ArrayExtensions
	{
		/// <summary>
		/// Assets/throws if an array is not exactly the provided <see cref="expectedLength"/>.
		/// </summary>
		/// <typeparam name="TElementType"></typeparam>
		/// <param name="array">Array to check.</param>
		/// <param name="expectedLength">The expected exact length.</param>
		public static void AssertLengthExact<TElementType>(this TElementType[] array, int expectedLength)
		{
			if (array == null) throw new ArgumentNullException(nameof(array));

			if (array.Length != expectedLength)
				throw new InvalidOperationException($"Length of {nameof(array)} is not expected Length: {expectedLength}");
		}
	}
}
