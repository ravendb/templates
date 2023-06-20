# RavenDB Azure Functions Template (Node.js TypeScript)

[![Deploy to Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplate-azure-functions-typescript%2Fmaster%2Fazuredeploy.json)

[![Visualize](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/visualizebutton.svg?sanitize=true)](http://armviz.io/#/?load=https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplate-azure-functions-typescript%2Fmaster%2Fazuredeploy.json)

A batteries included template for kick starting a Node.js TypeScript Azure Functions project that connects with a RavenDB database backend.

[RavenDB][cloud-signup] is a NoSQL document database for distributed applications offering industry-leading security without compromising performance. With a RavenDB database you can set up a NoSQL data architecture or add a NoSQL layer to your current relational database.

> The easiest way to get started with RavenDB is by creating [a free RavenDB Cloud account][cloud-signup] or requesting a free license to [download it yourself][download].
>
> If you are _brand new_ to RavenDB, we recommend starting with the [Getting Started guide][docs-get-started], the [RavenDB bootcamp][learn-bootcamp], or the [Try RavenDB][learn-demo] experience.

## Use the Template

To create a `my-project` directory using this template, run:

```sh
npx degit ravendb/templates/azure-functions/node-http my-project
cd my-project
git init
```

To start the Azure Function:

```sh
npm install
func start
```

## Deploy to Azure

Click the "Deploy to Azure" button above to deploy the ARM template for this repository.

You can also [manually create an Azure Functions app][az-func-deploy].

## Configuring the Template

Watch the video walkthrough tutorial or read through [the step-by-step guide in the RavenDB docs][docs-howto] that covers how to get up and running successfully with this template.


### RavenDB Settings

The template uses environment variables to store connection information to RavenDB.

Update the `local.settings.json` file:

```json
{
  "DB_URLS": "https://a.free.mycompany.ravendb.cloud,https://b.free.mycompany.ravendb.cloud",
  "DB_NAME": "demo",
  "DB_CERT_PATH": "../certs/a.free.mycompany.client.certificate.pfx"
}
```

* `DB_URLS` contains a comma-separated list of cluster node URLs to connect to.
* `DB_NAME` is the database name to connect to.
* `DB_CERT_PATH` is the path to the certificate, relative to the `.csproj` file or an absolute file path.

### Loading certificates

#### Using `.pfx` files locally

> ❗ **Do not** store a certificate password in the `local.settings.json` file as it will be committed to source control.

Overall, you want to avoid including the `.pfx` in source control whether it is password-protected or not. This means you can pass an absolute or relative file path to `DB_CERT_PATH` that sits outside your Git repository. You will need to define an environment variable for `DB_CERT_PASSWORD` if using the password-protected `.pfx` otherwise you can use the certificate file without a password.

#### Using App Configuration in Azure

In the Azure Portal, you will need to define a `DB_CERT_PEM` app configuration. This will contain BOTH your public and private key in plaintext (stored encrypted in the portal).

You can copy/paste the contents of your `.pem` file, which will look like this:

```
-----BEGIN CERTIFICATE-----
MIIFCzCC...
-----END CERTIFICATE-----
-----BEGIN RSA PRIVATE KEY-----
MIIJKAI...
-----END RSA PRIVATE KEY-----
```

The template will handle this for you and pass it to the `DocumentStore`.

#### Using Azure Key Vault

Optionally you could store the certificate in Azure Key Vault, retrieve the bytes, and pass the buffer to the DocumentStore. This incurs extra cost both in terms of storage and network latency.

[cloud-signup]: https://cloud.ravendb.net?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=cloud_signup
[download]: https://ravendb.net/download?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=download
[docs-get-started]: https://ravendb.net/docs/article-page/nodejs/start/getting-started?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=docs_get_started
[docs-create-db]: https://ravendb.net/docs/article-page/csharp/studio/database/create-new-database/general-flow?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=docs_new_db
[learn-bootcamp]: https://ravendb.net/learn/bootcamp?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=learn_bootcamp
[learn-demo]: https://demo.ravendb.net/?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=learn_demo
[docs-howto]: https://ravendb.net/docs/article/nodejs/start/platform-guides/azure-functions/overview?utm_source=github&utm_medium=web&utm_campaign=github_template_cloudflare_worker&utm_content=docs_howto
[az-func-deploy]: https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp?tabs=in-process#deploy-the-project-to-azure
