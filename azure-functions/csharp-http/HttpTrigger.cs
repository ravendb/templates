using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;

namespace Company.FunctionApp
{
  public class HttpTrigger
  {
    private readonly IAsyncDocumentSession session;

    public HttpTrigger(IAsyncDocumentSession session)
    {
      this.session = session;
    }

    [FunctionName("HttpTrigger")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
      log.LogInformation("C# HTTP trigger function processed a request.");

      var node = await session.Advanced.GetCurrentSessionNode();

      string responseMessage = string.IsNullOrEmpty(node.ClusterTag)
          ? "Successfully connected to RavenDB"
          : $"Successfully connected to RavenDB - Node {node.ClusterTag}";

      return new OkObjectResult(responseMessage);
    }
  }
}
