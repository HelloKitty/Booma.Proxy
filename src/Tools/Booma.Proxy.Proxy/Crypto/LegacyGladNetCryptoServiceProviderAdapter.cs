using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Booma
{
	public sealed class LegacyGladNetCryptoServiceProviderAdapter : GladNet.ICryptoServiceProvider
	{
		private Booma.ICryptoServiceProvider AdaptedProvider { get; }

		public LegacyGladNetCryptoServiceProviderAdapter([NotNull] Booma.ICryptoServiceProvider adaptedProvider)
		{
			AdaptedProvider = adaptedProvider ?? throw new ArgumentNullException(nameof(adaptedProvider));
		}

		public byte[] Crypt(byte[] bytes)
		{
			return AdaptedProvider.Crypt(bytes);
		}

		public byte[] Crypt(byte[] bytes, int offset, int count)
		{
			return AdaptedProvider.Crypt(bytes, offset, count);
		}

		public int BlockSize => AdaptedProvider.BlockSize;
	}
}
