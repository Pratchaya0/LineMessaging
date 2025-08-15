using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineMessaging.API
{
	internal class PerUserRichMenuAPI
	{
		private const string LinkRichMenuToUser = "user/{userId}/richmenu/{richMenuId}";
		private const string LinkRichMenuToMultipleUsers = "richmenu/bulk/link";
		private const string GetRichMenuIDOfUser = "user/{userId}/richmenu";
		private const string UnlinkRichMenuFromUser = "user/{userId}/richmenu";
		private const string UnlinkRichMenusFromMultipleUsers = "richmenu/bulk/unlink";
		private const string ReplaceOrUnlinkTheLinkedRichMenusInBatches = "richmenu/batch";
		private const string GetTheStatusOfRichMenuBatchVontrol = "richmenu/progress/batch";
		private const string ValidateARequestOfRichMenuBatchControl = "richmenu/validate/batch";
	}
}
