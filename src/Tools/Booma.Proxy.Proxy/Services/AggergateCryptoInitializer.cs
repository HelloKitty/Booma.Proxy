using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace Booma.Proxy
{
	public sealed class AggergateCryptoInitializer : GladNet.ICryptoKeyInitializable<byte[]>
	{
		private IEnumerable<GladNet.ICryptoKeyInitializable<byte[]>> AggergatedCryptoInitializables { get; }

		/// <inheritdoc />
		public AggergateCryptoInitializer(params GladNet.ICryptoKeyInitializable<byte[]>[] aggergatedCryptoInitializables)
		{
			AggergatedCryptoInitializables = aggergatedCryptoInitializables;
		}

		/// <inheritdoc />
		public AggergateCryptoInitializer(IEnumerable<GladNet.ICryptoKeyInitializable<byte[]>> aggergatedCryptoInitializables)
		{
			AggergatedCryptoInitializables = aggergatedCryptoInitializables;
		}

		/// <inheritdoc />
		public void Initialize(byte[] key)
		{
			foreach(var init in AggergatedCryptoInitializables)
				init.Initialize(key);
		}

		/// <inheritdoc />
		public void Uninitialize()
		{
			foreach(var init in AggergatedCryptoInitializables)
				init.Uninitialize();
		}
	}
}
