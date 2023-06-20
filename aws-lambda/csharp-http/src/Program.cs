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
      var useBinaryCert = certBytes != null;

      if (useBinaryCert)
      {
        var cert = new X509Certificate2(
          System.Text.Encoding.UTF8.GetBytes(certBytes),
          appConfig.RavenSettings.CertPassword
        );

        options.Certificate = cert;
      }

      var certPrivateKeyEncoded = appConfig.RavenSettings.CertPrivateKey;
      var certPublicKeyFilePath = appConfig.RavenSettings.CertPublicKeyFilePath;
      var usePemCert = certPrivateKeyEncoded != null && certPublicKeyFilePath != null;

      if (usePemCert)
      {
        var keyPem =
          System.Text.Encoding.UTF8.GetString(
            System.Convert.FromBase64String(certPrivateKeyEncoded)
          );
        var certPem = File.ReadAllText(certPublicKeyFilePath);
        // Workaround ephemeral keys in Windows
        // See: https://github.com/dotnet/runtime/issues/66283
        var intermediateCert = X509Certificate2.CreateFromPem(certPem, keyPem);
        var cert = new X509Certificate2(intermediateCert.Export(X509ContentType.Pfx));
        intermediateCert.Dispose();

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