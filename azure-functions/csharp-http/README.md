# RavenDB Azure Functions Template (.NET C#)

[![Deploy to Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplates%2Fmain%2Fazure-functions%2Fcsharp-http%2Fazuredeploy.json)

[![Visualize](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/visualizebutton.svg?sanitize=true)](http://armviz.io/#/?load=https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplates%2Fmain%2Fazure-functions%2Fcsharp-http%2Fazuredeploy.json)

A batteries included template for kick starting a C# Azure Functions app that connects with a RavenDB Cloud database backend.

[RavenDB][cloud-signup] is a NoSQL document database for distributed applications offering industry-leading security without compromising performance. With a RavenDB database you can set up a NoSQL data architecture or add a NoSQL layer to your current relational database.

> The easiest way to get started with RavenDB is by creating [a free RavenDB Cloud account][cloud-signup] or requesting a free license to [download it yourself][download].
>
> If you are _brand new_ to RavenDB, we recommend starting with the [Getting Started guide][docs-get-started], the [RavenDB bootcamp][learn-bootcamp], or the [Try RavenDB][learn-demo] experience.

## Use the Template

To create a `my-project` directory using this template, run one of the following sets of commands:

<details>
  <summary>Clone Command (npx):</summary> 
  
  ```sh
  npx degit ravendb/templates/azure-functions/csharp-http my-project; cd my-project; git init
  ```
</details>

<details>
  <summary>Clone Command (Bash):</summary> 
  
  ```sh
  git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/csharp-http; rm -rf .git; git init
  ```
</details>

<details>
  <summary>Clone Command (Powershell):</summary>

  ```sh
  git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/csharp-http; rm -r -force .git; git init
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
    "CertFilePath": "path/to/cert.pfx",
    "CertThumbprint": "<cert_thumbprint>" // Optional, Windows-only
  }
}
```

### Certificate Path (Windows / Linux)

The path to the certificate can be relative to the `.csproj` file or an absolute file path.

### Storing the `CertPassword` secret

To store `RavenSettings:CertPassword`, you can use User Secrets locally and Azure Settings when deployed.

```bash
dotnet user-secrets init
dotnet user-secrets set RavenSettings:CertPassword "<PASSWORD>"
```

In Azure Portal, add a `RavenSettings__CertPassword` app setting.

### Using a Certificate Thumbprint (Windows-only)

Specifying the `RavenSettings:CertThumbprint` will search the Windows Certificate Store under `My\CurrentUser` for the specified certificate.

1. In Azure Portal, under Function App "Certificates", upload the `.pfx` with password
1. Set the `WEBSITE_LOAD_CERTIFICATES` app setting to the thumbprint value
1. Set the `RavenSettings__CertThumbprint` app setting to the thumbprint value

### Using Azure Key Vault

Optionally you could store the certificate in Azure Key Vault, retrieve the bytes, and build the `X509Certificate2` to provide to the DocumentStore.

[cloud-signup]: https://cloud.ravendb.net?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_csharp&utm_content=cloud_signup
[download]: https://ravendb.net/download?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_csharp&utm_content=download
[docs-get-started]: https://ravendb.net/docs/article-page/csharp/start/getting-started?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_csharp&utm_content=docs_get_started
[docs-create-db]: https://ravendb.net/docs/article-page/csharp/studio/database/create-new-database/general-flow?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_csharp&utm_content=docs_new_db
[learn-bootcamp]: https://ravendb.net/learn/bootcamp?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_csharp&utm_content=learn_bootcamp
[learn-demo]: https://demo.ravendb.net/?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_csharp&utm_content=learn_demo
[docs-howto]: https://ravendb.net/docs/article/csharp/start/guides/azure-functions/overview?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_csharp&utm_content=docs_howto
[nuget-ravendb-di]: https://www.nuget.org/packages/RavenDB.DependencyInjection 
[az-func-deploy]: https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp?tabs=in-process#deploy-the-project-to-azure
