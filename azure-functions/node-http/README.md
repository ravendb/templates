# RavenDB Azure Functions Template (Node.js TypeScript)

[![Deploy to Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplate-azure-functions-typescript%2Fmaster%2Fazuredeploy.json)

[![Visualize](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/visualizebutton.svg?sanitize=true)](http://armviz.io/#/?load=https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplate-azure-functions-typescript%2Fmaster%2Fazuredeploy.json)

A batteries included template for kick starting a Node.js TypeScript Azure Functions project that connects with a RavenDB Cloud database backend.

[RavenDB][cloud-signup] is a business continuity database for distributed applications offering industry-leading security with sub-100ms query performance.

> The easiest way to get started with RavenDB is by creating [a free RavenDB Cloud account](cloud-signup).
>
> If you are _brand new_ to RavenDB, we recommend starting with the [Getting Started guide](docs-get-started), the [RavenDB bootcamp](learn-bootcamp), or the [Try RavenDB](learn-demo) experience.

## Use the Template

To create a `my-project` directory using this template, run:

```sh
$ git clone https://github.com/ravendb/template-azure-functions-typescript my-project
$ cd my-project
$ func start
```

To start the Azure Function:

```sh
$ func start
```

## Deploy to Azure

Click the "Deploy to Azure" button above to deploy the ARM template for this repository.

You can also [manually create an Azure Functions app][az-func-deploy].

## Configuring the Template

Watch the video walkthrough tutorial or read through [the step-by-step guide in the RavenDB docs][docs-howto] that covers how to get up and running successfully with this template.

[cloud-signup]: https://cloud.ravendb.net?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=cloud_signup
[docs-get-started]: https://ravendb.net/docs/article-page/nodejs/start/getting-started?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=docs_get_started
[docs-create-db]: https://ravendb.net/docs/article-page/csharp/studio/database/create-new-database/general-flow?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=docs_new_db
[learn-bootcamp]: https://ravendb.net/learn/bootcamp?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=learn_bootcamp
[learn-demo]: https://demo.ravendb.net/?utm_source=github&utm_medium=web&utm_campaign=github_template_az_func_typescript&utm_content=learn_demo
[docs-howto]: https://ravendb.net/docs/article/nodejs/start/platform-guides/azure-functions/overview?utm_source=github&utm_medium=web&utm_campaign=github_template_cloudflare_worker&utm_content=docs_howto
[az-func-deploy]: https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp?tabs=in-process#deploy-the-project-to-azure
