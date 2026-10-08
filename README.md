# AqarFlow – Real Estate CRM

AqarFlow is a Real Estate Customer Relationship Management (CRM) system built using ASP.NET Core MVC, Entity Framework Core, and SQL Server.

The system helps real estate marketers manage customers, properties, follow-up activities, property interests, and deals in one centralized platform.

## Technologies Used

- C# and ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- HTML, CSS, JavaScript, and Bootstrap
- Swagger / OpenAPI

## Project Architecture

The solution contains five projects:

| Project | Description |
|---|---|
| AqarFlow | MVC web application and user interface |
| AqarFlow.API | REST API endpoints |
| AqarFlow.Application | Business services, interfaces, and DTOs |
| AqarFlow.Domain | Domain entities and models |
| AqarFlow.Infrastructure | Data access, repositories, and Unit of Work |

## Main Features

- Customer Management
- Property Management
- Customer Follow-ups
- Deal Management
- Customer Property Interests
- User Management
- Roles and Permissions Management
- User Authentication
- Arabic User Interface

## REST API

The API provides CRUD endpoints for:

- Customers
- Properties
- FollowUps
- Deals
- CustomerPropertyInterests

API documentation is available through Swagger when running the API project locally.

## How to Run

1. Clone the repository.
2. Open `AqarFlow.slnx` in Visual Studio.
3. Restore NuGet packages.
4. Configure the SQL Server connection string in the application settings.
5. Prepare the `AqarFlowDB` database using the project's EF Core configuration and migrations.
6. Run the `AqarFlow` project to access the MVC website.
7. Run `AqarFlow.API` separately to access the REST API and Swagger.

## Project Purpose

AqarFlow was developed as a practical training project to demonstrate ASP.NET Core MVC development, database integration, CRUD operations, layered architecture, and REST API development.
