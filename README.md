# EMixCepFinder

EMixCepFinder is a solution for finding and retrieving information about Brazilian postal codes (CEP). It consists of an ASP.NET Core API project, a Domain project for business logic, an Infrastructure project for data access, and a Service project for external integrations.

## How to Use

1. Clone the repository to your local machine.
2. Use a command prompt to navigate to the root directory of the repository.
3. Run the following commands to set up the database:
   ```
   dotnet ef migrations add {MigrationName} --project EMixCepFinder.Infrastructure --startup-project EMixCepFinder.API
   dotnet ef database update --project EMixCepFinder.Infrastructure --startup-project EMixCepFinder.API
   ```
   Note: Replace `{MigrationName}` with a name for the migration.
4. In Visual Studio, set the the solution Startup Projects to `EMixCepFinder.API` and `EMixCepFinder.ConsoleApplication`.
5. Build and run the solution in Visual Studio.
6. Use an HTTP client (e.g. Postman) or Swagger to make requests to the API endpoints.

## Dependencies

EMixCepFinder has the following dependencies:

### EMixCepFinder.API

- ASP.NET Core 3.1
- Swashbuckle.AspNetCore
- Refit

### EMixCepFinder.Infrastructure

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools

### EMixCepFinder.Service

- Refit

## API Flow

The EMixCepFinder API has the following endpoints:

- `GET /api/addressinfo/{cep}`: Retrieves information about a CEP, such as street name, city, and state. The API uses Refit to make requests to the ViaCep API and then returns the response to the client.
- `GET /api/addressinfo/state/{state}`: Retrieves all addresses in a given state. The API returns a list of addresses matching the given state code.

## License

This project is licensed under the MIT License.

## Project Structure

The solution is divided into four main projects:

- `EMixCepFinder.API`: This project is responsible for handling HTTP requests and returning HTTP responses to the client. It is the entry point to the application.
- `EMixCepFinder.Domain`: This project contains the business logic of the application. It defines the entities, interfaces, and services that are used by other projects.
- `EMixCepFinder.Infrastructure`: This project contains the implementation of the database access and other infrastructure concerns. It contains the data models and repository implementations.
- `EMixCepFinder.Service`: This project is responsible for integrating with external services. It contains the implementation of the Refit clients that are used to make requests to external APIs and internal logic.

## Design Patterns and Best Practices

The EMixCepFinder project follows several design patterns and best practices:

- Dependency Injection: All dependencies are injected using .NET Core's built-in dependency injection container. This makes it easy to replace implementations of interfaces with mock objects during testing.
- Repository Pattern: The `EMixCepFinder.Infrastructure` project implements the repository pattern to abstract away the details of data storage from the rest of the application. This makes it easy to change the data storage implementation without affecting the rest of the application.
- CQRS Pattern: The `EMixCepFinder.Domain` project implements the Command Query Responsibility Segregation (CQRS) pattern to separate read and write operations. This makes it easier to reason about the system and optimize for performance.
- FluentValidation: The `EMixCepFinder.Domain` project uses FluentValidation to
