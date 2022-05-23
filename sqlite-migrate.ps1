$migrationName=$args[0];
dotnet tool install --global dotnet-ef

dotnet-ef migrations add $migrationName --context ApplicationDbContext --startup-project AvcolForms.Web --project AvcolForms.Core.Data.Sqlite --configuration release