using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineMessaging.Model.Message
{
	internal interface IMessage
	{
		MessageType Type { get; }
	}
}
