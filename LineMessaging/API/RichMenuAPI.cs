using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineMessaging.API
{
	internal class RichMenuAPI
	{
		private const string CreateRichMenuPath = "richmenu";
		private const string ValidateRichMenuObjectPath = "richmenu/validate";
		private const string UploadRichMenuImagePath = "richmenu/{richMenuId}/content";
		private const string DownloadRichMenuImage = "richmenu/{richMenuId}/content";
		private const string GetRichMenuList = "richmenu/list";
		private const string GetRichMenu = "richmenu/{richMenuId}";
		private const string DeleteRichMenu = "richmenu/{richMenuId}";
		private const string SetDefaultRichMenu = "user/all/richmenu/{richMenuId}";
		private const string GetDefaultRichMenuID = "user/all/richmenu";
		private const string ClearDefaultRichMenu = "user/all/richmenu";
	}
}
