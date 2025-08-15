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
	public enum TemplateType
	{
		[EnumMember(Value = "buttons")]
		Buttons,

		[EnumMember(Value = "confirm")]
		Confirm,

		[EnumMember(Value = "carousel")]
		Carousel,

		[EnumMember(Value = "image_carousel")]
		ImageCarousel,
	}
}
