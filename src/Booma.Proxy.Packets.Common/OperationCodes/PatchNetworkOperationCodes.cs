using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booma.Proxy
{
	//Based on Syl: https://github.com/Sylverant/patch_server/blob/master/src/patch_packets.h#L136
	/// <summary>
	/// Enumeration of the patching network operation codes.
	/// </summary>
	public enum PatchNetworkOperationCodes
	{
		//TODO: Adjust formatting
		PATCH_WELCOME_TYPE              = 0x0002,
		PATCH_LOGIN_TYPE                = 0x0004,
		PATCH_FILE_SEND                 = 0x0006,
		PATCH_DATA_SEND                 = 0x0007,
		PATCH_FILE_DONE                 = 0x0008,
		PATCH_SET_DIRECTORY             = 0x0009,
		PATCH_ONE_DIR_UP                = 0x000A,
		PATCH_START_LIST                = 0x000B,
		PATCH_FILE_INFO                 = 0x000C,
		PATCH_INFO_FINISHED             = 0x000D,
		PATCH_FILE_INFO_REPLY           = 0x000F,
		PATCH_FILE_LIST_DONE            = 0x0010,
		PATCH_SEND_INFO                 = 0x0011,
		PATCH_SEND_DONE                 = 0x0012,
		PATCH_MESSAGE_TYPE              = 0x0013,
		PATCH_REDIRECT_TYPE             = 0x0014,
		PATCH_REDIRECT6_TYPE            = 0x0614
	}
}
