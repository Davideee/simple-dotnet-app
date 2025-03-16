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

3. **Add a New Migration:**
    When you have made changes to your data model and want to apply them to the database, you need to create a new migration. To create a new migration, run the following command:

    ```bash
    dotnet ef migrations add MigrationName --project .\Model --startup-project .\Web
    ```

    - `MigrationName`: Replace this with a descriptive name for your migration (e.g., `AddNewFieldToSomeEntity`).
    - `--project .\Model`: The project that contains the Entity Framework Core models.
    - `--startup-project .\Web`: The startup project to be used when running the migration.

    Example:

    ```bash
    dotnet ef migrations add AddNewFieldToCustomer --project .\Model --startup-project .\Web
    ```

4. **Apply the Migration to the Database:**
    Once the migration is created, you can apply the changes to the database by running the following command:

    ```bash
    dotnet ef database update --project .\Model --startup-project .\Web
    ```

    - This command applies all migrations that have not yet been applied to the database, including the migration you just added.

    Example:

    ```bash
    dotnet ef database update --project .\Model --startup-project .\Web
    ```
	
If you want to setup a new database with EF do following step:

    ```bash
    dotnet ef database update --connection "Host=YOUR_HOST;Port=YOUR_PORT;Database=YOUR_DB;Username=YOUR_USER;Password=YOUR_PW;Timeout=10;"  --project .\Model --startup-project .\Web
    ```


## Notes:

- **Manage multiple migrations:** If you add multiple migrations and want to apply a specific migration at a later time, you can specify the name of the migration:

    ```bash
    dotnet ef database update MigrationName --project .\Model --startup-project .\Web
    ```

    This applies only the specified migration and all previous migrations.

- **Undo migrations:** If you need to undo a migration, you can use the command `dotnet ef migrations remove`. This removes the last migration:

    ```bash
    dotnet ef migrations remove --project .\Model --startup-project .\Web
    ```

- **Using custom migration configurations:** You can also make custom migration configurations in the `DbContext` class if special settings are required for your migrations.

---

With these additions, you can now create, manage and apply new migrations!