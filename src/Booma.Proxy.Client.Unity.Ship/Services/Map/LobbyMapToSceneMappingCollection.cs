using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Booma.Proxy
{
	/// <summary>
	/// Collection that maps the lobby id to scene file names.
	/// </summary>
	[Serializable]
	public sealed class LobbyMapToSceneMappingCollection : IDictionary<int, string>
	{
		[NonSerialized]
		private IDictionary<int, string> LobbyMapping = new Dictionary<int, string>()
		{
			{0, "Lobby01" },
			{2, "Lobby01" },

			{14, "LobbySoccer01" }
		};

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<int, string>> GetEnumerator()
		{
			return LobbyMapping.GetEnumerator();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)LobbyMapping).GetEnumerator();
		}

		/// <inheritdoc />
		public void Add(KeyValuePair<int, string> item)
		{
			LobbyMapping.Add(item);
		}

		/// <inheritdoc />
		public void Clear()
		{
			LobbyMapping.Clear();
		}

		/// <inheritdoc />
		public bool Contains(KeyValuePair<int, string> item)
		{
			return LobbyMapping.Contains(item);
		}

		/// <inheritdoc />
		public void CopyTo(KeyValuePair<int, string>[] array, int arrayIndex)
		{
			LobbyMapping.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(KeyValuePair<int, string> item)
		{
			return LobbyMapping.Remove(item);
		}

		/// <inheritdoc />
		public int Count => LobbyMapping.Count;

		/// <inheritdoc />
		public bool IsReadOnly => LobbyMapping.IsReadOnly;

		/// <inheritdoc />
		public bool ContainsKey(int key)
		{
			return LobbyMapping.ContainsKey(key);
		}

		/// <inheritdoc />
		public void Add(int key, string value)
		{
			LobbyMapping.Add(key, value);
		}

		/// <inheritdoc />
		public bool Remove(int key)
		{
			return LobbyMapping.Remove(key);
		}

		/// <inheritdoc />
		public bool TryGetValue(int key, out string value)
		{
			return LobbyMapping.TryGetValue(key, out value);
		}

		/// <inheritdoc />
		public string this[int key]
		{
			get => LobbyMapping[key];
			set => LobbyMapping[key] = value;
		}

		/// <inheritdoc />
		public ICollection<int> Keys => LobbyMapping.Keys;

		/// <inheritdoc />
		public ICollection<string> Values => LobbyMapping.Values;
	}
}
