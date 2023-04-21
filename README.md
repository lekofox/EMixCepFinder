# EMixCepFinder

EMixCepFinder is a solution for finding and retrieving information about Brazilian postal codes (CEP). It consists of an ASP.NET Core API project, a Domain project for business logic, an Infrastructure project for data access, and a Service project for external integrations.

## How to Use

1. Clone the repository to your local machine.
2. At PackageManagerConsole type Add-Migration {MigrationName}
3. Still at PackageManagerConsole, type updata-database
4. Change StartupProject on solution explorer to EmixCepFinder.API
5. Run the EMixCepFinder.API project in Visual Studio.
6. Use an HTTP client (e.g. Postman) or Swagger to make requests to the API endpoints.

## Dependencies

EMixCepFinder has the following dependencies:

### EMixCepFinder.API

- ASP.NET Core 3.1
- Swashbuckle.AspNetCore
- Refit

### EMixCepFinder.Domain

- FluentValidation

### EMixCepFinder.Infrastructure

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools

### EMixCepFinder.Service

- Refit

## API Flow

The EMixCepFinder API has the following endpoints:

- `GET /api/addressinfo/{cep}`: Retrieves information about a CEP, such as street name, city, and state.

The API uses Refit to make requests to the ViaCep API and then returns the response to the client.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
