using Amazon.Lambda.Core;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Raven.Client.Documents.Session;
using Raven.Client.ServerWide.Operations;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MyCompany.Functions;

public class Functions
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public Functions()
  {
  }

  /// <summary>
  /// A simple function that takes a string and does a ToUpper
  /// </summary>
  /// <param name="session"></param>
  /// <param name="context"></param>
  /// <returns></returns>
  [LambdaFunction()]
  [HttpApi(LambdaHttpMethod.Get, "/")]
  public async Task<IHttpResult> FunctionHandler([FromServices] IAsyncDocumentSession session, ILambdaContext context)
  {
    var welcomeData = new WelcomeData()
    {
      url = session.Advanced.DocumentStore.Urls[0],
      database = session.Advanced.DocumentStore.Database,
      functionName = context.FunctionName,
      invocationId = context.AwsRequestId,
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

    return HttpResults.Ok(content)
      .AddHeader("content-type", "text/html");
  }
}
