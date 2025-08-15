using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LineMessaging
{
	public partial class LineMessagingApiClient : IDisposable
	{
		private readonly RestClient _client;
		private readonly RestClient _dataClient;
		private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
		};

		private const string APIBaseUrl = "https://api.line.me/v2/bot";
		private const string APIDataBaseUrl = "https://api-data.line.me/v2/bot";

		public LineMessagingApiClient(string channelAccessToken) : this(new RestClient(APIBaseUrl), new RestClient(APIDataBaseUrl), channelAccessToken)
		{
		}

		public LineMessagingApiClient(RestClient client, RestClient dataClient, string channelAccessToken)
		{
			_client = client ?? throw new ArgumentNullException(nameof(client));
			_dataClient = dataClient ?? throw new ArgumentNullException(nameof(dataClient));

			// Add headers for authentication
			_client.AddDefaultHeader("Authorization", $"Bearer {channelAccessToken}");
			_dataClient.AddDefaultHeader("Authorization", $"Bearer {channelAccessToken}");
		}

		public void Dispose()
		{
			_client.Dispose();
			_dataClient.Dispose();
		}
	}
}
