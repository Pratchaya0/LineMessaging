using LineMessaging.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineMessaging.API
{
	public partial class LineMessagingClient
	{
		private const string SendReplyMessagePath = "message/reply";
		private const string SendPushMessagePath = "message/push";
		private const string SendMulticastMessagePath = "message/multicast";
		private const string SendNarrowcastMessagePath = "message/narrowcast";
		private const string GetNarrowcastMessageStatusPath = "message/progress/narrowcast";
		private const string SendBroadcastMessagePath = "message/broadcast";
		private const string DisplayALoadingAnimationPath = "chat/loading/start";
		private const string GetTheTargetLimitForSendingMessagesThisMonthPath = "message/quota";
		private const string GetMumberOfMessagesSentThisMonthPath = "message/quota/consumption";
		private const string GetNumberOfSentReplyMessagesPath = "message/delivery/reply";
		private const string GetNumberOfSentPushMessagesPath = "/message/delivery/push";
		private const string GetNumberOfSentMulticastMessagesPath = "message/delivery/multicast";
		private const string GetNumberOfSentBroadcastMessagesPath = "message/delivery/broadcast";
		private const string ValidateMessageObjectsOfAReplyMessagePath = "message/validate/reply";
		private const string ValidateMessageObjectsOfAPushMessagePath = "message/validate/push";
		private const string ValidateMessageObjectsOfAMulticastMessagePath = "message/validate/multicast";
		private const string ValidateMessageObjectsOfANarrowcastMessagePath = "message/validate/narrowcast";
		private const string ValidateMessageObjectsOfABroadcastMessagePath = "message/validate/broadcast";
	}
}
