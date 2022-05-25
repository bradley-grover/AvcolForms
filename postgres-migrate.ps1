$migrationName=$args[0];

dotnet-ef migrations add $migrationName --context ApplicationDbContext --startup-project AvcolForms.Web --project AvcolForms.Core.Data.Postgres --configuration release