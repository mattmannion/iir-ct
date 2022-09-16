using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace tests;

[TestClass]
public class GetEventTests
{
  // Test Mocks
  HttpRequest request = Mock.Of<HttpRequest>();
  ILogger logger = Mock.Of<ILogger>();

  // When given a valid URL Param the result is OK 
  [TestMethod]
  public async Task TestsGetEventOkResponse()
  {
    IActionResult result = await GetEvent.GetEvent.Run(request, logger, 100);

    Assert.AreEqual(
      JsonConvert.SerializeObject(
        new OkObjectResult(new
        {
          event_name = "December 1st Event",
          event_length = "09h 00m 00s",
          event_url = "https://www.valorforblue.org/Events/Dec-1-Event"
        })
      ),
      JsonConvert.SerializeObject(result)
    );
  }

  // When given a valid URL Param the result is NotFound 
  [TestMethod]
  public async Task TestsGetEventNotFoundResponse()
  {
    IActionResult result = await GetEvent.GetEvent.Run(request, logger, 200);

    Assert.AreEqual(
      JsonConvert.SerializeObject(new NotFoundObjectResult(
        new { error = "No event found, please try another id." }
        )
      ),
      JsonConvert.SerializeObject(result)
    );
  }
}