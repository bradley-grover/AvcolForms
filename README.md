# Avcol Forms
Blazor Server App used for managing and creating forms

![GitHub](https://img.shields.io/github/license/ac111897/AvcolForms)
![GitHub last commit](https://img.shields.io/github/last-commit/ac111897/AvcolForms)
![GitHub repo size](https://img.shields.io/github/repo-size/ac111897/AvcolForms)

# Status
![GitHub Workflow Status](https://img.shields.io/github/workflow/status/ac111897/AvcolForms/.NET)
![GitHub Workflow Status (branch)](https://img.shields.io/github/workflow/status/ac111897/AvcolForms/.NET/development?label=dev-build)
![Website](https://img.shields.io/website?down_color=red&label=website-docs&up_message=online%21&url=https%3A%2F%2Facvolforms-docs.ac111897.repl.co)
![GitHub deployments](https://img.shields.io/github/deployments/ac111897/AvcolForms/avcol-forms?label=deployment)

# Installation/Running
## Prerequisites:
- .NET 6 runtime
- Powershell Script Exectuion Access (Optional), this is to generate migrations faster you don't actually need this, although to run ef scripts you should build in release

## Notes:
- Before running the project I advise you to configure some things as you may not like the defaults
### Database & Entity Framework Core:
- If your run your project in debug it will use [an in memory provider](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.InMemory)
- If you run in release it will use the selected provider and run the `Database.EnsureCreated()` function which creates the db if it doesn't exist already
- Db-Provider in [./appsettings.json](https://github.com/ac111897/AvcolForms/master/AvcolForms.Web/appsettings.json)
- Once the provider is set you can use the powershell scripts in the project root to create a migration
- You should run `./setup.ps1` if you don't have Entity Framework Tools installed, or if you need to update them as you might need to update them for new versions of the app
- To run for a provider the syntax is `./[provider-name]-migrate.ps1 [migration-name]`
- For example `./sqlite-migrate.ps1 InitialCreate`


## Git Commands:
### Running the web project
1. ```git clone https://github.com/ac111897/AvcolForms.git```
2. ```cd AvcolForms```
3. ```cd AvcolForms.Web```
4. ```dotnet run```
### Running the unit tests
1. ```git clone https://github.com/ac111897/AvcolForms.git```
2. ```cd AvcolForms```
3. ```cd AvcolForms.Tests```
4. ```dotnet test```

## Other README Files
[Web Project README](https://github.com/ac111897/AvcolForms/tree/master/AvcolForms.Web/README.MD)</br>
[Core Library README](https://github.com/ac111897/AvcolForms/tree/master/AvcolForms.Core/README.MD)</br>
[Data Library README](https://github.com/ac111897/AvcolForms/tree/master/AvcolForms.Core.Data/README.MD)</br>

### Libraries Used
<img src="./resources/dotnet-logo.png" width="100" height="100" alt="dotnet" width="18%"><img 
                                                                                              src="./resources/mudblazor-logo.png" width="100" height="100" alt="MudBlazor" width="18%"><img 
                                                                                                    src="./resources/blazor-logo.png" width="100" height="100" alt="Blazor" width="18%">
