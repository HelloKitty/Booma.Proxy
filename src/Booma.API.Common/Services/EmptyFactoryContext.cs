using System;
using System.Collections.Generic;
using System.Text;

namespace Guardians
{
	public sealed class EmptyFactoryContext
	{
		private static EmptyFactoryContext instance;
		private static object SyncObj = new object();

		public static EmptyFactoryContext Instance
		{
			get
			{
				if(instance == null)
				{
					lock(SyncObj)
					{
						if(instance == null)
						{
							instance = new EmptyFactoryContext();
						}
					}
				}
				return instance;
			}
		}

		private EmptyFactoryContext()
		{
			
		}
	}
}
