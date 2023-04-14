using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide.Operations;

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
        ILogger log,
        ExecutionContext context)
    {
      log.LogInformation("C# {0} function processed a request.", context.FunctionName);

      var welcomeData = new WelcomeData()
      {
        url = session.Advanced.DocumentStore.Urls[0],
        database = session.Advanced.DocumentStore.Database,
        functionName = context.FunctionName,
        invocationId = context.InvocationId.ToString(),
        connected = true,
      };

      try
      {
        var buildOpResult = await session.Advanced.DocumentStore.Maintenance.Server.SendAsync(
          new GetBuildNumberOperation()
        );
        welcomeData.version = buildOpResult.ProductVersion;
      }
      catch (Exception ex)
      {
        welcomeData.connected = false;
        welcomeData.error = ex;
      }

      var content = await WelcomeTemplate.RenderHtmlAsync(welcomeData);

      return new ContentResult()
      {
        Content = content,
        ContentType = "text/html",
        StatusCode = 200
      };
    }
  }
}
