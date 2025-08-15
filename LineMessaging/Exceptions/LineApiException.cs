using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LineMessaging.Exceptions
{
	public class LineApiException : Exception
	{
		public HttpStatusCode StatusCode { get; }
		public string ErrorCode { get; }

		public LineApiException(HttpStatusCode statusCode, string message, string errorCode = null)
			: base(message)
		{
			StatusCode = statusCode;
			ErrorCode = errorCode;
		}
	}

	public class LineApiBadRequestException : LineApiException
	{
		public LineApiBadRequestException(string message)
			: base(HttpStatusCode.BadRequest, message, "BAD_REQUEST") { }
	}

	public class LineApiUnauthorizedException : LineApiException
	{
		public LineApiUnauthorizedException(string message = "Valid channel access token is not specified")
			: base(HttpStatusCode.Unauthorized, message, "UNAUTHORIZED") { }
	}

	public class LineApiForbiddenException : LineApiException
	{
		public LineApiForbiddenException(string message = "Not authorized to access the resource")
			: base(HttpStatusCode.Forbidden, message, "FORBIDDEN") { }
	}

	public class LineApiNotFoundException : LineApiException
	{
		public LineApiNotFoundException(string message = "Unable to get profile information")
			: base(HttpStatusCode.NotFound, message, "NOT_FOUND") { }
	}

	public class LineApiConflictException : LineApiException
	{
		public LineApiConflictException(string message = "An API request with the same retry key has already been accepted")
			: base(HttpStatusCode.Conflict, message, "CONFLICT") { }
	}

	public class LineApiGoneException : LineApiException
	{
		public LineApiGoneException(string message = "Access to the resource that is no longer available")
			: base(HttpStatusCode.Gone, message, "GONE") { }
	}

	public class LineApiPayloadTooLargeException : LineApiException
	{
		public LineApiPayloadTooLargeException(string message = "Request exceeds the max size of 2MB")
			: base(HttpStatusCode.RequestEntityTooLarge, message, "PAYLOAD_TOO_LARGE") { }
	}

	public class LineApiUnsupportedMediaTypeException : LineApiException
	{
		public LineApiUnsupportedMediaTypeException(string message = "Media type of the uploaded file is unsupported")
			: base(HttpStatusCode.UnsupportedMediaType, message, "UNSUPPORTED_MEDIA_TYPE") { }
	}

	public class LineApiTooManyRequestsException : LineApiException
	{
		public LineApiTooManyRequestsException(string message = "Too many requests - rate limit exceeded")
			: base((HttpStatusCode)429, message, "TOO_MANY_REQUESTS") { }
	}

	public class LineApiInternalServerErrorException : LineApiException
	{
		public LineApiInternalServerErrorException(string message = "Error on the internal server")
			: base(HttpStatusCode.InternalServerError, message, "INTERNAL_SERVER_ERROR") { }
	}

	// Status code handler utility class
	public static class LineApiStatusCodeHandler
	{
		/// <summary>
		/// Handles HTTP response and throws appropriate LINE API exceptions based on status codes
		/// </summary>
		/// <param name="response">The HTTP response message</param>
		/// <param name="responseContent">Optional response content for additional context</param>
		/// <exception cref="LineApiException">Throws specific LINE API exception based on status code</exception>
		public static async Task HandleResponseAsync(HttpResponseMessage response, string responseContent = null)
		{
			if (response.IsSuccessStatusCode)
				return;

			// Get response content if not provided
			responseContent ??= await response.Content.ReadAsStringAsync();

			switch (response.StatusCode)
			{
				case HttpStatusCode.BadRequest:
					throw new LineApiBadRequestException($"Problem with the request. Details: {responseContent}");

				case HttpStatusCode.Unauthorized:
					throw new LineApiUnauthorizedException("Valid channel access token is not specified");

				case HttpStatusCode.Forbidden:
					throw new LineApiForbiddenException("Not authorized to access the resource. Confirm that your account or plan is authorized to access the resource");

				case HttpStatusCode.NotFound:
					throw new LineApiNotFoundException(
						"Unable to get profile information. Possible reasons:\n" +
						"• Target user ID doesn't exist\n" +
						"• The user hasn't consented to their profile information being obtained\n" +
						"• The user hasn't added the target LINE Official Account as a friend\n" +
						"• The user blocked the target LINE Official Account after adding it as a friend");

				case HttpStatusCode.Conflict:
					throw new LineApiConflictException("An API request with the same retry key has already been accepted");

				case HttpStatusCode.Gone:
					throw new LineApiGoneException("Access to the resource that is no longer available");

				case HttpStatusCode.RequestEntityTooLarge:
					throw new LineApiPayloadTooLargeException("Request exceeds the max size of 2MB. Make the request smaller than 2MB and try again");

				case HttpStatusCode.UnsupportedMediaType:
					throw new LineApiUnsupportedMediaTypeException("Media type of the uploaded file is unsupported");

				case (HttpStatusCode)429: // Too Many Requests
					throw new LineApiTooManyRequestsException(
						"Too Many Requests:\n" +
						"• Exceeded the rate limit for requests\n" +
						"• Exceeded the limit on the number of concurrent operations for requests\n" +
						"• Exceeded the number of free messages\n" +
						"• Exceeded your maximum number of additional messages allowed to be sent");

				case HttpStatusCode.InternalServerError:
					throw new LineApiInternalServerErrorException($"Error on the internal server. Response: {responseContent}");

				default:
					throw new LineApiException(response.StatusCode,
						$"Unexpected status code: {(int)response.StatusCode} {response.StatusCode}. Response: {responseContent}");
			}
		}

		/// <summary>
		/// Handles HTTP response with detailed error information parsing
		/// </summary>
		/// <param name="response">The HTTP response message</param>
		/// <param name="parseErrorDetails">Function to parse error details from response content</param>
		public static async Task HandleResponseWithDetailsAsync(HttpResponseMessage response,
			Func<string, (string message, string errorCode)> parseErrorDetails = null)
		{
			if (response.IsSuccessStatusCode)
				return;

			var responseContent = await response.Content.ReadAsStringAsync();

			// Parse error details if parser is provided
			if (parseErrorDetails != null)
			{
				try
				{
					var (message, errorCode) = parseErrorDetails(responseContent);
					ThrowSpecificException(response.StatusCode, message, errorCode);
					return;
				}
				catch (Exception ex) when (!(ex is LineApiException))
				{
					// If parsing fails, fall back to default handling
				}
			}

			// Fall back to standard handling
			await HandleResponseAsync(response, responseContent);
		}

		private static void ThrowSpecificException(HttpStatusCode statusCode, string message, string errorCode)
		{
			switch (statusCode)
			{
				case HttpStatusCode.BadRequest:
					throw new LineApiBadRequestException(message);
				case HttpStatusCode.Unauthorized:
					throw new LineApiUnauthorizedException(message);
				case HttpStatusCode.Forbidden:
					throw new LineApiForbiddenException(message);
				case HttpStatusCode.NotFound:
					throw new LineApiNotFoundException(message);
				case HttpStatusCode.Conflict:
					throw new LineApiConflictException(message);
				case HttpStatusCode.Gone:
					throw new LineApiGoneException(message);
				case HttpStatusCode.RequestEntityTooLarge:
					throw new LineApiPayloadTooLargeException(message);
				case HttpStatusCode.UnsupportedMediaType:
					throw new LineApiUnsupportedMediaTypeException(message);
				case (HttpStatusCode)429:
					throw new LineApiTooManyRequestsException(message);
				case HttpStatusCode.InternalServerError:
					throw new LineApiInternalServerErrorException(message);
				default:
					throw new LineApiException(statusCode, message, errorCode);
			}
		}
	}

	// Example usage class
	public class LineApiClient
	{
		private readonly HttpClient _httpClient;
		private readonly string _channelAccessToken;

		public LineApiClient(HttpClient httpClient, string channelAccessToken)
		{
			_httpClient = httpClient;
			_channelAccessToken = channelAccessToken;
		}

		public async Task<string> GetUserProfileAsync(string userId)
		{
			try
			{
				// Set up the request
				var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.line.me/v2/bot/profile/{userId}");
				request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _channelAccessToken);

				// Make the request
				var response = await _httpClient.SendAsync(request);

				// Handle status codes
				await LineApiStatusCodeHandler.HandleResponseAsync(response);

				// If successful, return the content
				return await response.Content.ReadAsStringAsync();
			}
			catch (LineApiNotFoundException ex)
			{
				// Handle specific case where user profile is not found
				Console.WriteLine($"User profile not found: {ex.Message}");
				throw;
			}
			catch (LineApiUnauthorizedException ex)
			{
				// Handle authorization issues
				Console.WriteLine($"Authorization error: {ex.Message}");
				throw;
			}
			catch (LineApiTooManyRequestsException ex)
			{
				// Handle rate limiting
				Console.WriteLine($"Rate limited: {ex.Message}");
				// You might want to implement retry logic here
				throw;
			}
			catch (LineApiException ex)
			{
				// Handle any other LINE API exceptions
				Console.WriteLine($"LINE API error {ex.StatusCode}: {ex.Message}");
				throw;
			}
		}

		// Example method with retry logic for rate limiting
		public async Task<string> GetUserProfileWithRetryAsync(string userId, int maxRetries = 3)
		{
			for (int attempt = 1; attempt <= maxRetries; attempt++)
			{
				try
				{
					return await GetUserProfileAsync(userId);
				}
				catch (LineApiTooManyRequestsException) when (attempt < maxRetries)
				{
					// Wait before retrying (exponential backoff)
					await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempt)));
				}
			}

			throw new LineApiTooManyRequestsException("Maximum retry attempts exceeded");
		}
	}
}
