# Project Context for Jules

## Tech Stack
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- xUnit for testing

## Project Structure
- `/Controllers` → API controllers (e.g., `ProductController`)
- `/Services` → Business logic (e.g., `ProductService`)
- `/Data` → EF Core DbContext and migrations
- `/Models` → Entity and DTO classes
- `/Tests` → Unit and integration tests

## Coding Guidelines
- Follow Microsoft C# Coding Conventions
- Use async/await for all I/O operations
- All API controllers must return `IActionResult`
- Use dependency injection for all services
- Apply proper HTTP status codes for each endpoint
- Swagger must be enabled in Development environment

## Known Issues
- No input validation on Product creation/update
- No pagination support for `GetAll` endpoint

## Example Tasks for Jules
- "Add input validation in ProductController for Create and Update endpoints"
- "Implement pagination in GetAll endpoint of ProductController"
- "Add integration tests for DELETE /api/product/{id} endpoint"
- "Refactor ProductService to separate mapping logic into DTO mappers"
- "Update EF Core to the latest stable version"
