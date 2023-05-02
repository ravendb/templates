using System.Security.Cryptography.X509Certificates;
using Raven.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add configuration support for AWS Secrets Manager
// builder.Configuration.AddSecretsManager();

// Strongly-type app config.
builder.Services.Configure<AppConfiguration>(builder.Configuration);

builder.Services.AddRavenDbAsyncSession();
builder.Services.AddRavenDbDocStore(options =>
    {
      var appConfig = builder.Configuration.Get<AppConfiguration>();
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

builder.Services.AddControllers();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();