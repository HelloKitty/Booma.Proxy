using System;
using System.Collections.Generic;
using System.Text;

namespace Guardians
{
	//TODO: Doc
	public interface IUIAdapterRegisterable
	{
		UnityUIRegisterationKey RegisterationKey { get; }

		/// <summary>
		/// The actual type to register this UI adapter as.
		/// </summary>
		Type UISerivdeType { get; }
	}
}
