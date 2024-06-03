 # RavenDB Templates
 [RavenDB][cloud-signup] is a NoSQL document database for distributed applications offering industry-leading security without compromising performance. With a RavenDB database you can set up a NoSQL data architecture or add a NoSQL layer to your current relational database.

> The easiest way to get started with RavenDB is by creating [a free RavenDB Cloud account][cloud-signup] or requesting a free license to [download it yourself][download].
>
> If you are _brand new_ to RavenDB, we recommend starting with the [Getting Started guide][docs-get-started], the [RavenDB bootcamp][learn-bootcamp], or the [Try RavenDB][learn-demo] experience.

Get started quickly with RavenDB using different framework and service integrations.

| Template | Deploy  | Watch Tutorial | Clone Template Command |
| -------- | ------- | -------------- | ---------------------- |
| [AWS Lambda .NET (C#)](aws-lambda/csharp-http) | | [![How To Use AWS Lambda with RavenDB .NET](https://img.youtube.com/vi/T2r9sqrTrYE/1.jpg)](https://www.youtube.com/watch?v=T2r9sqrTrYE) | <details><summary>Clone Command (npx):</summary> `npx degit ravendb/templates/aws-lambda/csharp-http my-project; cd my-project; git init`</details> <details><summary>Clone Command (Bash):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter aws-lambda/csharp-http; rm -rf .git; git init`</details> <details><summary>Clone Command (Powershell):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter aws-lambda/csharp-http; rm -r -force .git; git init`</details>|
| [Azure Functions .NET (C#)](azure-functions/csharp-http) | [![Deploy to Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplates%2Fmain%2Fazure-functions%2Fcsharp-http%2Fazuredeploy.json) | [![Using Azure Functions with RavenDB .NET](https://img.youtube.com/vi/1vnpfsD3bSE/1.jpg)](https://www.youtube.com/watch?v=1vnpfsD3bSE) | <details><summary>Clone Command (npx):</summary> `npx degit ravendb/templates/azure-functions/csharp-http my-project; cd my-project; git init`</details> <details><summary>Clone Command (Bash):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/csharp-http; rm -rf .git; git init`</details> <details><summary>Clone Command (Powershell):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/csharp-http; rm -r -force .git; git init`</details>|
| [Azure Functions Node.js (TypeScript)](azure-functions/node-http) | [![Deploy to Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplates%2Fmain%2Fazure-functions%2Fnode-http%2Fazuredeploy.json) | [![Using Azure Functions with RavenDB Node.js](https://img.youtube.com/vi/TJdJ3TJK-Sg/1.jpg)](https://www.youtube.com/watch?v=TJdJ3TJK-Sg) | <details><summary>Clone Command (npx):</summary> `npx degit ravendb/templates/azure-functions/node-http my-project; cd my-project; git init`</details> <details><summary>Clone Command (Bash):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/node-http; rm -rf .git; git init`</details> <details><summary>Clone Command (Powershell):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/node-http; rm -r -force .git; git init`</details>|
| [Cloudflare Worker (TypeScript)](https://github.com/ravendb/template-cloudflare-worker) | [![Deploy to Cloudflare Workers](https://deploy.workers.cloudflare.com/button)](https://deploy.workers.cloudflare.com/?url=https://github.com/ravendb/template-cloudflare-worker) | [![Using Cloudflare Workers with RavenDB](https://img.youtube.com/vi/qMJfgQicjwk/1.jpg)](https://www.youtube.com/watch?v=qMJfgQicjwk) | <details><summary>Clone Command (npx):</summary> `npx degit ravendb/template-cloudflare-worker my-project; cd my-project; git init`</details> <details><summary>Clone Command:</summary> `git clone https://github.com/ravendb/template-cloudflare-worker my-project; cd my-project; git init`</details>|

## Cloning the Templates

If you don't want to use a deployment wizard or need to clone the templates locally, here are two ways:

### If you have Node.js and `npx` installed:

You can use [degit](https://github.com/Rich-Harris/degit) to quickly scaffold a template:

```sh
npx degit ravendb/templates/<template_dir> [new_repo_name]
```

For example:

```sh
$ npx degit ravendb/templates/azure-functions/node-http my-project
$ cd my-project
$ git init
```

If the project directory is omitted, the template will be cloned in the current directory.

### Manually using git

This is more involved but has 3 steps:

1. Clone the repository into a new folder
1. Filter the subtree to only the subdirectory of the template
1. Reset Git

The commands to do this are:

```sh
$ git clone git clone https://github.com/ravendb/templates <new_repo_name>
$ git filter-branch --subdirectory-filter <template_dir>
$ rm -rf .git # or `rm -r -force .git` on Windows
$ git init
```

Above in the table there is a clone command box you can copy from and paste directly into the terminal that concatenates these into one command sequence.


## Configure the Templates

You can find [detailed how-to guides and videos][docs-howto] on the RavenDB docs for each template.

[cloud-signup]: https://cloud.ravendb.net?utm_source=github&utm_medium=web&utm_campaign=github_templates_home&utm_content=cloud_signup
[download]: https://ravendb.net/download?utm_source=github&utm_medium=web&utm_campaign=github_templates_home&utm_content=download
[docs-get-started]: https://ravendb.net/docs/article-page/csharp/start/getting-started?utm_source=github&utm_medium=web&utm_campaign=github_templates_home&utm_content=docs_get_started
[learn-bootcamp]: https://ravendb.net/learn/bootcamp?utm_source=github&utm_medium=web&utm_campaign=github_templates_home&utm_content=learn_bootcamp
[learn-demo]: https://demo.ravendb.net/?utm_source=github&utm_medium=web&utm_campaign=github_templates_home&utm_content=learn_demo
[docs-howto]: https://ravendb.net/docs/article-page/csharp/getting-started/guides/?utm_source=github&utm_medium=web&utm_campaign=github_templates_home&utm_content=docs_howto
