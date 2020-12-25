﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booma.Proxy;

namespace Booma
{
	public interface IOperationCodeable<TOperationCodeType>
	public interface IOperationCodeable<out TOperationCodeType>
		where TOperationCodeType : Enum
	{
		TOperationCodeType OperationCode { get; }
	}
}
