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

		/// <summary>
		/// If -1 it means they never had a previous zone.
		/// </summary>
		public int OldZoneId { get; }

		public int NewZoneId { get; }

		public PlayerZoneAreaWarpedEventArgs(byte identifier, int oldZoneId, int newZoneId)
		{
			if (identifier <= 0) throw new ArgumentOutOfRangeException(nameof(identifier));
			if (newZoneId <= 0) throw new ArgumentOutOfRangeException(nameof(newZoneId));

			Identifier = identifier;
			OldZoneId = oldZoneId;
			NewZoneId = newZoneId;
		}

		public PlayerZoneAreaWarpedEventArgs(byte identifier, int newZoneId)
			: this(identifier, -1, newZoneId)
		{
			if(identifier <= 0) throw new ArgumentOutOfRangeException(nameof(identifier));
			if(newZoneId <= 0) throw new ArgumentOutOfRangeException(nameof(newZoneId));

			Identifier = identifier;
			NewZoneId = newZoneId;
		}
	}
}
