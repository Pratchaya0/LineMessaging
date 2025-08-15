using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LineMessaging.Common
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EventType
	{
		[EnumMember(Value = "postback")]
		Postback,

		[EnumMember(Value = "message")]
		Message,

		[EnumMember(Value = "uri")]
		Uri,

		[EnumMember(Value = "datetimepicker")]
		Datetimepicker
	}
}
