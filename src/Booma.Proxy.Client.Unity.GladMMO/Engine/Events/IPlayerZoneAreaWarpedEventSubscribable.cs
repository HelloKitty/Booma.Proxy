using System;
using System.Collections.Generic;
using System.Text;
using GladMMO;

namespace Booma.Proxy
{
	public interface IPlayerZoneAreaWarpedEventSubscribable
	{
		event EventHandler<PlayerZoneAreaWarpedEventArgs> OnPlayerZoneAreWarped;
	}


	public class PlayerZoneAreaWarpedEventArgs : EventArgs, Booma.Proxy.IMessageContextIdentifiable
	{
		public byte Identifier { get; }

		public int OldZoneId { get; }

		public int NewZoneId { get; }

		public PlayerZoneAreaWarpedEventArgs(byte identifier, int oldZoneId, int newZoneId)
		{
			if (identifier <= 0) throw new ArgumentOutOfRangeException(nameof(identifier));
			if (oldZoneId <= 0) throw new ArgumentOutOfRangeException(nameof(oldZoneId));
			if (newZoneId <= 0) throw new ArgumentOutOfRangeException(nameof(newZoneId));

			Identifier = identifier;
			OldZoneId = oldZoneId;
			NewZoneId = newZoneId;
		}
	}
}
