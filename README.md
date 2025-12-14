# MySaaS

A Software-as-a-Service (SaaS) platform for restaurants, bars and hospitality businesses to manage purchases, sales, inventory, staff (waiters), and more.

This repository is a personal, full-stack .NET project intended as my first major product and as a showcase for potential employers to evaluate my technical skills, architectural choices and commitment to software engineering best practices.

## Overview

Key domains and features:
- Purchase management (suppliers, purchase orders, costs)
- Sales management (orders, bills, payment methods)
- Stock and inventory (stock movements, products, low-stock alerts)
- Staff management (waiters, roles, shifts, performance)
- Multi-branch/multi-tenant readiness (planned)
- Reporting and basic analytics
- Role-based access control and audit logging

## Technology Stack

- Platform: `.NET 10` (C#)
- API: `ASP.NET Core Web API`
- Architecture: Clean / Hexagonal style with clear separation into layers (`MySaaS.Domain`, `MySaaS.Application`, `MySaaS.Infrastructure`, `MySaaS.WebApi`)
- Data access: `Dapper` (lightweight ADO.NET mapper; hand-written SQL and repositories)
- Mapping: Manual mapping implemented in application mappers
- Validation: `FluentValidation` (or inline validation where practical)
- Logging: `Serilog` (structured logging)
- Testing: `xUnit` for unit and integration tests (`MySaaS.Tests`)

> Notes: Docker, CI/CD and automated pipelines are not used yet — these are planned for future iterations.

## Architecture and Design Goals

- Clean separation of concerns: domain entities and business rules live in `MySaaS.Domain`, application logic and use cases in `MySaaS.Application`, persistence and external integration in `MySaaS.Infrastructure`, and the host API in `MySaaS.WebApi`.
- DDD-inspired modeling for complex business logic (aggregates, value objects, domain events where needed).
- Testable code: dependency injection, small single-responsibility classes, and unit/integration test coverage.
- Explicit, hand-written data access and mapping to keep control over SQL and performance (Dapper + manual mappers).
- Extensibility: designed so features (e.g., new payment providers, reporting backends) can be added with minimal changes.
- Observability: structured logging and clear error handling to make debugging and monitoring straightforward.
- Security: Role-based access control and secure handling of credentials; secrets via environment variables.

## Solution Structure

- `MySaaS.Domain` — Core entities, value objects, domain services and business rules.
- `MySaaS.Application` — DTOs, manual mappers, use case handlers, validation and application services.
- `MySaaS.Infrastructure` — Dapper-based repositories, SQL scripts, database access utilities and external integrations.
- `MySaaS.WebApi` — REST API controllers, DI composition, authentication/authorization, and application host.
- `MySaaS.Tests` — Unit and integration tests demonstrating expected behavior and contracts.

## Running the project (local)

Prerequisites:
- `.NET 10 SDK`
- A SQL Server or other supported RDBMS instance reachable from your machine

Quick start:
1. Clone the repo
   `git clone <repo-url>`
2. Restore and build
   `dotnet restore`
   `dotnet build`
3. Prepare the database
   - Run the provided SQL scripts in `MySaaS.Infrastructure/DbScripts` (or run your own schema setup) to create tables and seed data.
   - Ensure the connection string in `MySaaS.WebApi/appsettings.Development.json` or environment variables points to your database.
4. Run the API
   - From repository root: `cd MySaaS.WebApi` then `dotnet run`
5. Run tests
   `dotnet test MySaaS.Tests`

Environment configuration: use `appsettings.Development.json` and environment variables for connection strings, secrets and feature flags.

## Development Practices & Quality

- Follow SOLID principles and clean code practices.
- Use small, focused pull requests and descriptive commit messages.
- Maintain unit and integration tests for critical flows (purchases, sales, stock adjustments).
- Use code reviews and static analysis where practical.

## CI/CD and Deployment

- CI/CD and containerization are not in use yet. Future plans include GitHub Actions and Docker for consistent builds and deployments.

## What an employer should know about me (project commitments)

- I design systems with maintainability and testability as first-class goals.
- I apply DDD / clean architecture to keep business rules isolated from framework code.
- I write automated tests and will adopt CI to keep the codebase healthy.
- I invest in readable, documented code and pragmatic engineering trade-offs.
- This repository is a working sandbox: features will be added iteratively with an emphasis on correctness and clarity.

## How to contribute / contact

- Branching strategy: feature branches from `dev`, PRs to `dev`, and PRs from `dev` to `main` when ready for release.
- Open an issue for bug reports or feature requests and submit a PR with tests and documentation for changes.

## License

This project is provided under the `MIT` license. See `LICENSE` for details.

---