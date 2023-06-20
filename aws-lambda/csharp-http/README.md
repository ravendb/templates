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

For more, see [Cloning the Templates](../../README.md#cloning-the-templates).

You will need to have your AWS environment set up. The easiest method is to use the AWS Toolkit extension for Visual Studio or Visual Studio Code. You can use it to sign in and set up your `~/.aws/credentials` file. You will also need to define a `AWS_REGION` environment variable in your shell profile or environment.

To run the Lambda function locally:

```sh
cd src
dotnet restore
dotnet run
```

> NOTE: This does not use any simulated Lambda runtime. You may want to use the AWS .NET Mock Lambda tool to test anything Lambda-specific.

## Deploy to AWS Lambda

### Manually

You can deploy your function manually through the AWS Lambda .NET Tools:

```sh
dotnet lambda deploy-function <csproj>
```

Running this for the first time will walk you through setting up your IAM function role.

### Automatically Using CI/CD

The template is configured to deploy automatically through GitHub Actions workflows for CI/CD. You will need the following secrets and variables:

- **Secret:** `AWS_ACCESS_KEY_ID` -- Your IAM deployment user Access Key ID
- **Secret:** `AWS_ACCESS_KEY_SECRET` -- Your IAM deployment user Access Key Secret (password-equivalent)
- **Variable:** `AWS_REGION` -- The AWS region to deploy to (e.g. `us-east-1`)
- **Variable:** `AWS_LAMBDA_FUNCTION_NAME` -- The AWS Lambda function name, typically the name of the `.csproj` file (e.g. `RavenDBLambda`)
- **Variable:** `AWS_LAMBDA_FUNCTION_ROLE` -- The IAM role assigned to your AWS Lambda function, created on initial deployment or manually

## Configuring the Template

Watch the video walkthrough tutorial or read through [the step-by-step guide in the RavenDB docs][docs-howto] that covers how to get up and running successfully with this template.

### RavenDB Settings

The template uses [RavenDB.DependencyInjection][nuget-ravendb-di] to configure RavenDB.

Update the `appsettings.json` or `appsettings.development.json` files:

```json
{
  "RavenSettings": {
    "Urls": ["http://live-test.ravendb.net"],
    "DatabaseName": "Northwind"
  }
}
```

#### Using a PFX/X509 Certificate locally

Use the `CertFilePath` to use a PFX file locally, **stored outside the repository.**

The path to the certificate can be relative to the `.csproj` file or an absolute file path.

> ❗ Avoid storing PFX files in your repository and deploying them. Use the contents of your PEM file instead, see below.

#### Using a PEM Certificate

Instead of uploading a PFX file, in your AWS Lambda function, you will need to use the `RavenSettings:CertPublicKeyFilePath` setting and define a `RavenSettings__CertPrivateKey` environment variable. AWS limits environment variables to less than 5KB so to avoid incurring the extra cost of AWS Secrets Manager, you can use a public key committed to source control (safe) with a base64-encoded private key.

You can specify the `CertPublicKeyFilePath` as an app setting or environment variable (`RavenSettings__CertPublicKeyFilePath`):

```json
{
  "RavenSettings": {
    "Urls": ["https://a.free.mycompany.ravendb.cloud"],
    "DatabaseName": "Northwind",
    "CertPublicKeyFilePath": "free.mycompany.client.certificate.crt"
  }
}
```

The path should be relative to your `.csproj` file. The contents of your `.crt` file will look like this:

```
-----BEGIN CERTIFICATE-----
MIIFCzCC...
-----END CERTIFICATE-----
```

`.crt` files are automatically copied to your output and publish directories on build.

Then, you need to [base64 encode](https://www.base64encode.org/) the contents of your `.key` file, which will look like this:

```
-----BEGIN RSA PRIVATE KEY-----
MIIJKAI...
-----END RSA PRIVATE KEY-----

=> LS0tLS1CRUdJTiBSU0EgUFJJVkFURSBLRVktLS0tLQpNSUlKS0FJLi4uCi0tLS0tRU5EIFJTQSBQUklWQVRFIEtFWS0tLS0t
```

Use the encoded value for `CertPrivateKey`. Pass this in as an environment variable in AWS, like:

```
RavenSettings__CertPrivateKey=<encoded private key>
```

> ❗ Don't commit the private key to source control as an app setting. Base64 encoding is not encryption.

The template will handle this for you and pass it to the `DocumentStore`.

### Using AWS Secrets Manager

[AWS Secrets Manager][aws-secrets] is a secure way to store encrypted configuration without relying on environment variables. This incurs extra cost and is enabled through the [Kralizek.Extensions.Configuration.AWSSecretsManager][aws-secrets-nuget] package. If you do not wish to use AWS Secrets Manager, you can safely uninstall it.

To enable loading configuration from AWS Secrets Manager, in `Program.cs`, uncomment the following line:

```csharp
// builder.Configuration.AddSecretsManager();
```

> ❗ You will need to grant the `secretsmanager:readwrite` permission set on the Lambda IAM user, otherwise you will run into a runtime exception.

To store and load configuration, create a secret key corresponding to the app setting. For example, here is `RavenSettings` stored as a JSON object:

```json
{
  "RavenSettings": {
    "Urls": ["https://a.free.mycompany.ravendb.cloud"],
    "DatabaseName": "Northwind",
    "CertPublicKeyFilePath": "free.mycompany.client.certificate.crt",
    "CertPrivateKey": "<base64 encoded private key>"
  }
}
```

For more, read the guide on [setting up AWS Secrets Manager with RavenDB][docs-aws-secrets].

[cloud-signup]: https://cloud.ravendb.net?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=cloud_signup
[download]: https://ravendb.net/download?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=download
[docs-get-started]: https://ravendb.net/docs/article-page/csharp/start/getting-started?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=docs_get_started
[learn-bootcamp]: https://ravendb.net/learn/bootcamp?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=learn_bootcamp
[learn-demo]: https://demo.ravendb.net/?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=learn_demo
[docs-howto]: https://ravendb.net/docs/article/csharp/start/guides/aws-lambda/overview?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=docs_howto
[docs-howto-secrets]: https://ravendb.net/docs/article/csharp/start/guides/aws-lambda/secrets-manager?utm_source=github&utm_medium=web&utm_campaign=github_template_aws_lambda_csharp_http&utm_content=docs_howto_secrets
[nuget-ravendb-di]: https://www.nuget.org/packages/RavenDB.DependencyInjection
[aws-secrets]: https://aws.amazon.com/secrets-manager/
[aws-secrets-nuget]: https://www.nuget.org/packages/Kralizek.Extensions.Configuration.AWSSecretsManager
