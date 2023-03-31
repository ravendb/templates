using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Raven.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

[assembly: FunctionsStartup(typeof(Company.FunctionApp.Startup))]

namespace Company.FunctionApp;

public class Startup : FunctionsStartup
{
  public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
  {
    FunctionsHostBuilderContext context = builder.GetContext();

    builder.ConfigurationBuilder
        .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
        .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
        .AddEnvironmentVariables();
  }

  public override void Configure(IFunctionsHostBuilder builder)
  {
    var context = builder.GetContext();

    builder.Services.AddRavenDbDocStore(options =>
  {
    var certThumbprint = context.Configuration.GetSection("RavenSettings:CertThumbprint").Value;

    if (!string.IsNullOrWhiteSpace(certThumbprint))
    {
      var cert = GetRavendbCertificate(certThumbprint);

      options.Certificate = cert;
    }
  });

    builder.Services.AddRavenDbAsyncSession();
  }

  private static X509Certificate2 GetRavendbCertificate(string certThumb)
  {
    X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
    certStore.Open(OpenFlags.ReadOnly);

    X509Certificate2Collection certCollection = certStore.Certificates
        .Find(X509FindType.FindByThumbprint, certThumb, false);

    // Get the first cert with the thumbprint
    if (certCollection.Count > 0)
    {
      X509Certificate2 cert = certCollection[0];
      return cert;
    }

    certStore.Close();

    throw new Exception($"Certificate {certThumb} not found.");
  }
}