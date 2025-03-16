# Project Setup for Entity Framework Migrations

This project uses Entity Framework Core for database migrations. Follow these steps to create the initial migration and update the database.

## Steps:

1. **Add the initial migration:**
   Run the following command to add the first migration:

   ```bash
   dotnet ef migrations add InitialCreate --project .\Model --startup-project .\Web
   ```

   - `--project .\Model`: Specifies the project that contains the Entity Framework models.
   - `--startup-project .\Web`: Specifies the startup project to be used when running the migrations.

2. **Update the database:**
   Run the following command to update the database with the latest migration:

   ```bash
   dotnet ef database update --project .\Model --startup-project .\Web
   ```

   - `--project .\Model`: Specifies the project that contains the migration files.
   - `--startup-project .\Web`: Specifies the startup project to be used when applying the migrations.

## Notes:

- Make sure the database connection is correctly configured in the `appsettings.json` (or in the environment variable).
- These commands assume that `dotnet ef` is properly installed. If not, you can install it with the following command:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

Good luck with using the migrations!
