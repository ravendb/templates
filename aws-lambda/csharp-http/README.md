# RavenDB AWS Lambda .NET C# Template

A batteries included template for kick starting a .NET C# Minimal Web API AWS Lambda app that connects with a RavenDB database backend.

[RavenDB][cloud-signup] is a NoSQL document database for distributed applications offering industry-leading security without compromising performance. With a RavenDB database you can set up a NoSQL data architecture or add a NoSQL layer to your current relational database.

> The easiest way to get started with RavenDB is by creating [a free RavenDB Cloud account][cloud-signup] or requesting a free license to [download it yourself][download].
>
> If you are _brand new_ to RavenDB, we recommend starting with the [Getting Started guide][docs-get-started], the [RavenDB bootcamp][learn-bootcamp], or the [Try RavenDB][learn-demo] experience.

## Use the Template

To create a `my-project` directory using this template, run one of the following sets of commands:

<details>
  <summary>Clone Command (npx):</summary> 
  
  ```sh
  npx degit ravendb/templates/aws-lambda/csharp-http my-project; cd my-project; git init
  ```
</details>

<details>
  <summary>Clone Command (Bash):</summary> 
  
  ```sh
  git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter aws-lambda/csharp-http; rm -rf .git; git init
  ```
</details>

<details>
  <summary>Clone Command (Powershell):</summary>

  ```sh
  git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter aws-lambda/csharp-http; rm -r -force .git; git init
  ```
</details>

For more, see [Cloning the Templates](../../README.md#cloning-the-templates)

To start the Azure Function:

```sh
$ dotnet restore
$ func start
```


## Deploy to Azure

Click the "Deploy to Azure" button above to deploy the ARM template for this repository.

You can also [manually create an Azure Functions app][az-func-deploy].

## Configuring the Template

Watch the video walkthrough tutorial or read through [the step-by-step guide in the RavenDB docs][docs-howto] that covers how to get up and running successfully with this template.

### RavenDB Settings

The template uses [RavenDB.DependencyInjection][nuget-ravendb-di] to configure RavenDB.

Update the `appsettings.json` or `appsettings.development.json` files:

```json
{
  "RavenSettings": {
    "Urls": ["http://live-test.ravendb.net"],
    "DatabaseName": "Northwind",
    "CertFilePath": "path/to/cert.pfx"
  }
}
```

### Certificate Path (Windows / Linux)

The path to the certificate can be relative to the `.csproj` file or an absolute file path.

### Storing the `CertPassword` secret

To store `RavenSettings:CertPassword`, you can use User Secrets locally and AWS Lambda when deployed.

```bash
dotnet user-secrets init
dotnet user-secrets set RavenSettings:CertPassword "<PASSWORD>"
```

In the AWS Lambda environment variables settings, add a `RavenSettings__CertPassword` variable.

> You do not need to "encrypt-in-transit." This requires a custom configuration provider. Instead, use AWS Secrets Manager.

### Using AWS Secrets Manager

[AWS Secrets Manager][aws-secrets] is a secure way to store encrypted configuration without relying on environment variables. This incurs extra cost and is enabled through the [Kralizek.Extensions.Configuration.AWSSecretsManager][aws-secrets-nuget] package. If you do not wish to use AWS Secrets Manager, you can safely uninstall it.

To enable loading configuration from AWS Secrets Manager, in `Program.cs`, uncomment the following line:

```csharp
// builder.Configuration.AddSecretsManager();
```

> ❗ You will need to grant the `secretsmanager:readwrite` permission set on the Lambda IAM user, otherwise you will run into a runtime exception.

To store and load configuration, create a secret key corresponding to the app setting. For example, here is `RavenSettings` stored as a JSON object:

TBD

Settings will be merged last, meaning you can use it to override specific settings versus loading everything.

Your certificate (.pfx) can be stored as a binary object under the `RavenSettings:CertBytes` setting and will be loaded by the template.

To upload the certificate, use the `aws` CLI:

[cloud-signup]: https://cloud.ravendb.net?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=cloud_signup
[download]: https://ravendb.net/download?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=download
[docs-get-started]: https://ravendb.net/docs/article-page/csharp/start/getting-started?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=docs_get_started
[learn-bootcamp]: https://ravendb.net/learn/bootcamp?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=learn_bootcamp
[learn-demo]: https://demo.ravendb.net/?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=learn_demo
[docs-howto]: https://ravendb.net/docs/article/csharp/start/platform-guides/aws-lambda/overview?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=docs_howto
[nuget-ravendb-di]: https://www.nuget.org/packages/RavenDB.DependencyInjection
[aws-secrets]: https://aws.amazon.com/secrets-manager/
[aws-secrets-nuget]: https://www.nuget.org/packages/Kralizek.Extensions.Configuration.AWSSecretsManager