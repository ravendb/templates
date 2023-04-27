using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Raven.DependencyInjection;
using Amazon.Lambda.Annotations;
using Microsoft.Extensions.Configuration;

namespace MyCompany.Functions;

[LambdaStartup]
public class Startup
{
  public void ConfigureServices(IServiceCollection services)
  {
    var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
    ?? "Production";

    // Add configuration support that uses appsettings, environment variables, and AWS Secrets Manager
    var config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{envName}.json", optional: true)
      .AddEnvironmentVariables()
      .AddSecretsManager()
      .Build();

    // Strongly-type app config.
    services.Configure<AppConfiguration>(config);

    // Add RavenDB support. When application loads configuration from AWS Secrets Manager, the certificate is provided in binary form and loaded
    services.AddRavenDbDocStore(options =>
    {
      var appConfig = config.Get<AppConfiguration>();
      var certBytes = appConfig.RavenSettings.CertBytes;

      if (certBytes != null)
      {
        var cert = new X509Certificate2(
          System.Text.Encoding.UTF8.GetBytes(certBytes),
          appConfig.RavenSettings.CertPassword
        );

        options.Certificate = cert;
      }
    });

    services.AddRavenDbAsyncSession();
  }
}