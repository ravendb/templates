using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide.Operations;

[ApiController]
[Route("")]
public class WelcomeController : ControllerBase
{

  private readonly ILogger<WelcomeController> _logger;
  private readonly IAsyncDocumentSession _session;

  public WelcomeController(IAsyncDocumentSession session,
  ILogger<WelcomeController> logger)
  {
    _session = session;
    _logger = logger;
  }

  [HttpGet(Name = "GetWelcome")]
  public async Task<ActionResult> Get()
  {
    var context = Request.HttpContext.Items[AbstractAspNetCoreFunction.LAMBDA_CONTEXT] as ILambdaContext;

    var welcomeData = new WelcomeData()
    {
      url = _session.Advanced.DocumentStore.Urls[0],
      database = _session.Advanced.DocumentStore.Database,
      functionName = context?.FunctionName ?? "",
      invocationId = context?.AwsRequestId ?? "",
      connected = true,
    };

    try
    {
      var buildOpResult = await _session.Advanced.DocumentStore.Maintenance.Server.SendAsync(
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

    return Content(content, "text/html");
  }
}