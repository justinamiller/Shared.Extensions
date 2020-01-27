using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq.Protected;
using System.Net;
using System.Net.Http.Headers;

namespace Dotnet.Extensions.TestFramework
{
	public class Person
	{
		public string Name { get; set; }

		public override bool Equals(object obj)
		{
			var input = (Person)obj;
			return input.Name == Name;
		}

		// generate hashcode
		public override int GetHashCode()
		{
			return !string.IsNullOrEmpty(Name)
									? Name.GetHashCode()
									: 0;
		}
	}

	[TestClass]
   public class ResponseMessageTest
    {
		[TestMethod]
		public async Task DeserializeTypedResponse_MustMatchInput()
		{
			var person = new Person { Name = "Lord Flashheart" };
			Person personAfterResponse;

			// create mocked HttpMessageHandler 
			var bounceInputHttpMessageHandlerMock = new Mock<HttpMessageHandler>();

			// set up the method
			bounceInputHttpMessageHandlerMock
				.Protected()
				.Setup<Task<HttpResponseMessage>>(
					"SendAsync",
					ItExpr.IsAny<HttpRequestMessage>(),
					ItExpr.IsAny<CancellationToken>()
				)
				.ReturnsAsync(new HttpResponseMessage()
				{
					StatusCode = HttpStatusCode.OK,
					Content = new StringContent(JsonConvert.SerializeObject(person))
				});

			// instantiate client
			var httpClient = new HttpClient(bounceInputHttpMessageHandlerMock.Object);

			// send some json
			var request = new HttpRequestMessage(HttpMethod.Post, "http://api/test")
			{
				Content = new StringContent(JsonConvert.SerializeObject(person))
			};
			request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			using (var response = await httpClient.SendAsync(request))
			{
				personAfterResponse = await response.DeserializeAsStreamAsync<Person>(new UTF8Encoding(), false, 1024, true);
			}

			Assert.AreEqual(person, personAfterResponse);
		}
	}
}
