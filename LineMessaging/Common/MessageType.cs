using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LineMessaging
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MessageType
	{
		[EnumMember(Value = "text")]
		Text,

		[EnumMember(Value = "image")]
		Image,

		[EnumMember(Value = "video")]
		Video,

		[EnumMember(Value = "audio")]
		Audio,

		[EnumMember(Value = "file")]
		File,

		[EnumMember(Value = "location")]
		Location,

		[EnumMember(Value = "sticker")]
		Sticker,

		[EnumMember(Value = "imagemap")]
		Imagemap,

		[EnumMember(Value = "template")]
		Template,

		[EnumMember(Value = "flex")]
		Flex,
	}
}
