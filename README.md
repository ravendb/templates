# RavenDB Templates

Get started quickly with [RavenDB](https://ravendb.net/docs) using different framework and service integrations.

| Template | Deploy | Clone Template Command |
| -------- | ------ | ---------------------- |
| [AWS Lambda .NET (C#)](aws-lambda/csharp-http) |  | <details><summary>Clone Command (Bash):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter aws-lambda/csharp-http; rm -rf .git; git init`</details> <details><summary>Clone Command (Powershell):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter aws-lambda/csharp-http; rm -r -force .git; git init`</details>|
| [Azure Functions .NET (C#)](azure-functions/csharp-http) | [![Deploy to Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplates%2Fmain%2Fazure-functions%2Fcsharp-http%2Fazuredeploy.json) | <details><summary>Clone Command (Bash):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/csharp-http; rm -rf .git; git init`</details> <details><summary>Clone Command (Powershell):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/csharp-http; rm -r -force .git; git init`</details>|
| [Azure Functions Node.js (TypeScript)](azure-functions/node-http) | [![Deploy to Azure](https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/1-CONTRIBUTION-GUIDE/images/deploytoazure.svg?sanitize=true)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fravendb%2Ftemplates%2Fmain%2Fazure-functions%2Fnode-http%2Fazuredeploy.json) | <details><summary>Clone Command (Bash):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/node-http; rm -rf .git; git init`</details> <details><summary>Clone Command (Powershell):</summary> `git clone https://github.com/ravendb/templates my-project; cd my-project; git filter-branch --subdirectory-filter azure-functions/node-http; rm -r -force .git; git init`</details>|

## Cloning the Templates

Git makes it hard to easily clone a subfolder of a repository but there are 3 main steps:

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