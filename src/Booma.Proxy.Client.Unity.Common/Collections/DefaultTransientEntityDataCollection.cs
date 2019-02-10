using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	public sealed class DefaultTransientEntityDataCollection : ICollection<IEntityCollectionRemovable>, ITransientEntityDataRemovableEnumerable
	{
		private List<IEntityCollectionRemovable> InternalRemovableCollection { get; }

		public DefaultTransientEntityDataCollection()
		{
			InternalRemovableCollection = new List<IEntityCollectionRemovable>(10);
		}

		/// <inheritdoc />
		public IEnumerator<IEntityCollectionRemovable> GetEnumerator()
		{
			return InternalRemovableCollection.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)InternalRemovableCollection).GetEnumerator();
		}

		/// <inheritdoc />
		public void Add(IEntityCollectionRemovable item)
		{
			InternalRemovableCollection.Add(item);
		}

		/// <inheritdoc />
		public void Clear()
		{
			InternalRemovableCollection.Clear();
		}

		/// <inheritdoc />
		public bool Contains(IEntityCollectionRemovable item)
		{
			return InternalRemovableCollection.Contains(item);
		}

		/// <inheritdoc />
		public void CopyTo(IEntityCollectionRemovable[] array, int arrayIndex)
		{
			InternalRemovableCollection.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(IEntityCollectionRemovable item)
		{
			return InternalRemovableCollection.Remove(item);
		}

		/// <inheritdoc />
		public int Count => InternalRemovableCollection.Count;

		/// <inheritdoc />
		public bool IsReadOnly => false;
	}
}
