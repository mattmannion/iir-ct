using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;

namespace GetEvent;

public static class GetEvent
{
  [FunctionName("GetEvent")]
  public static async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "get-event/{id:int}")] HttpRequest req,
      ILogger log, int id)
  {
    JSONEvent result = await JSONEvent.GetEvent("data/events.json", id);

    if (result is not null)
    {
      return new OkObjectResult(new { event_name = result.name, event_length = result.EventLength(), event_url = result.url });
    }
    else
    {
      return new NotFoundObjectResult(new { error = "No event found, please try another id." });
    }
  }
}

