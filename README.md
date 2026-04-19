# GamesRental

GamesRental is an ASP.NET Core MVC web application for renting physical game copies, managing a wishlist, and writing reviews for played games. The project extends the earlier course work with an Admin area, paging and search, service-layer tests, stronger authorization behavior, and clearer error handling.

## Live Demo

https://gamesrental.azurewebsites.net

## Concept

The application simulates a game rental platform where users can:

- browse a public catalog of available games
- search the catalog by title, genre, or platform
- view game details and all reviews
- rent available copies
- return rented games
- maintain a personal wishlist
- create one review per game

Administrators can:

- access a dedicated Admin area
- manage games
- manage genres
- manage platforms
- review simple statistics

## Main Features

- Public game catalog with pagination and search
- Game details page with review history
- ASP.NET Identity authentication and role-based authorization
- Admin area implemented with MVC Areas
- Rental workflow with active rentals and history
- Wishlist functionality
- Review functionality with duplicate-review prevention
- Seeded starter data for roles, admin user, genres, platforms, games, and copies
- Service-layer unit tests
- Custom 404 and 500 error pages

## Tech Stack

- ASP.NET Core MVC
- Razor Views and Razor Pages
- Entity Framework Core
- Microsoft SQL Server
- ASP.NET Core Identity
- Bootstrap 5
- NUnit
- Coverlet collector

## Architecture

The solution is split into multiple projects:

- `GamesRental` - web layer
- `GamesRental.Data` - EF Core context, migrations, seeding
- `GamesRental.Data.Models` - entity models
- `GamesRental.Services.Core` - business logic
- `GamesRental.Services.Common` - shared service abstractions/helpers
- `GamesRental.Web.ViewModels` - view models
- `GamesRental.Web.Infrastructure` - web helpers/extensions
- `GamesRental.Services.Tests` - service unit tests

This keeps business rules out of controllers and makes the service layer easier to test.

## Domain Model

Main entities:

- `ApplicationUser`
- `Game`
- `GameCopy`
- `Genre`
- `Platform`
- `Rental`
- `Review`
- `Wishlist`

## Validation and Security

- Data annotations are used on entities and view models
- Server-side validation is handled through `ModelState`
- Anti-forgery validation is applied to POST actions
- ASP.NET Identity manages users and roles
- Role-based checks protect the Admin area
- Razor output encoding helps prevent script injection in rendered content
- EF Core LINQ usage avoids SQL built from raw user input

## Seeding

On startup, the app seeds:

- `Admin` and `User` roles
- default admin account
- starter genres
- starter platforms
- starter games
- game copies

## Search and Pagination

The catalog supports:

- search by title, genre, or platform
- pagination with 6 games per page

## Testing

Current automated tests cover service scenarios such as:

- catalog filtering and paging
- game creation and copy creation
- rental and return flows
- duplicate-review prevention
- wishlist add/remove behavior

Test project:

- `GamesRental.Services.Tests`

Run tests with:

```powershell
dotnet test GamesRental.Services.Tests\GamesRental.Services.Tests.csproj
```

For final submission, it is recommended to generate and attach a coverage report and verify that service-layer coverage is at least 65%.

## Setup

1. Open the solution in Visual Studio 2022 or JetBrains Rider.
2. Update the connection string in `GamesRental/appsettings.json` if needed.
3. Apply migrations to the local SQL Server database.
4. Run the web project.

Example commands:

```powershell
dotnet ef database update --project GamesRental.Data --startup-project GamesRental
dotnet run --project GamesRental
```

## Azure Deployment

The project is ready to be deployed to Azure App Service with Azure SQL Database.

Recommended production setup:

1. Create an Azure App Service for the web project.
2. Create an Azure SQL Database and allow access from the App Service.
3. In Azure Portal, open the App Service and add a connection string named `DefaultConnection`.
4. Set `ASPNETCORE_ENVIRONMENT` to `Production`.
5. Apply the EF Core migrations against the Azure SQL database before first use.

Important notes:

- Do not store the production connection string directly in the repository.
- `GamesRental/appsettings.Production.json` is included as a safe template, but the real value for `DefaultConnection` should be set from Azure configuration.
- The local `appsettings.json` currently uses a local SQL Server connection string intended for development only.

Example migration command:

```powershell
dotnet ef database update --project GamesRental.Data --startup-project GamesRental
```

## Default Admin Account

- Email: `admin@site.com`
- Password: `Admin123!`

## User Roles

The application supports two user roles:

- `User` - can browse the catalog, rent and return games, maintain a wishlist, and create one review per game
- `Admin` - has access to the Admin area and can manage games, genres, platforms, and statistics

For coursework demonstration purposes, the default admin account is seeded automatically on startup.

## Repository and Deployment

Source repository:

- [GamesRental on GitHub](https://github.com/Exilemancer/GamesRental)

Live deployment URL:

- [GamesRental on Azure](https://gamesrental.azurewebsites.net)

Suggested deployment targets:

- Azure App Service
- IIS with SQL Server
- another public ASP.NET hosting provider

## Submission Checklist

- `Completed` - repository contains at least 30 meaningful commits
- `Completed` - README includes setup instructions, architecture overview, seeded admin account, and feature summary
- `Completed` - service-layer tests are included in `GamesRental.Services.Tests`
- `Completed` - live deployment URL is included in the README
- `Pending` - confirm all tests pass cleanly in the final local environment
- `Pending` - generate and attach a coverage report for the services
- `Optional` - add screenshots to the README
