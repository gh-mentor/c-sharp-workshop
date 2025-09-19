## Overview

This solution contains three .NET 8.0 projects:
- `DataLibrary`: Entity Framework Core data access layer plus the SQLite database (`SQLite/inventory.db`).
- `CoreWebApi`: ASP.NET Core Web API exposing product, category, and supplier data (currently two read endpoints for products).
- `CoreWebAPI.Tests`: MSTest project prepared for integration tests (future improvements planned).

Note: A former `prompts.md` file (prompt history / cheat sheet) has been removed. All relevant guidance and backlog items are now consolidated here.

## Current Features
- SQLite database created from `DataLibrary/scripts/inventory.sql` (tables: suppliers, categories, products; view: product_list).
- POCO entities: `Supplier`, `Category`, `Product` with EF Core fluent mappings in `InventoryDbContext`.
- Relationships: Category → Supplier (cascade), Product → Category (cascade).
- Web API: `GET /api/Inventory/Products` and `GET /api/Inventory/Products/{id}` with eager loading (Category + Supplier).
- Swagger (Development only), HTTPS redirection, DI-registered `InventoryDbContext` using dynamic path resolution (`AppContext.BaseDirectory/SQLite/inventory.db`).

## Enhancements – Not Yet Implemented
These represent the prioritized backlog. 

### API & Domain
- Add full CRUD for Products, Categories, Suppliers (POST/PUT/PATCH/DELETE) with validation.
- Introduce DTOs and AutoMapper (or manual projection) to avoid over-posting and control payload shape.
- Expose `product_list` view via a keyless EF Core entity (`HasNoKey`) and `GET /api/Inventory/ProductList`.

### Data & Migrations
- Align entity nullability with schema (e.g., nullable `ContactName`, `Price`, `Stock`) or enforce NOT NULL via migration.
- Add migrations and update pipeline to auto-apply (guarded in Production).
- Seed initial data via EF Core `ModelBuilder` or a startup seeding service instead of relying solely on the SQL script.

### Error Handling & Logging
- Add global exception handling middleware returning RFC 7807 ProblemDetails.
- Integrate structured logging (Serilog or built-in) with request correlation.
- Add validation problem responses (400) for malformed or invalid input models.

### Testing
- Refactor tests to use a custom `WebApplicationFactory` subclass consistently.
- Centralize seeding to avoid duplicate relationship insertion.
- Add integration tests for all CRUD endpoints and edge cases (404, 400, conflict, cascade delete behavior).
- Introduce unit tests for DbContext configuration (e.g., model validation) if warranted.

### Quality & Architecture
- Introduce a service layer or CQRS (MediatR) if complexity increases.
- Add pagination, filtering, and sorting for product queries.
- Implement optimistic concurrency (add a `RowVersion` byte[] column).

### Security & Hardening (Optional)
- Add authentication (JWT bearer) and role/claim-based authorization.
- Enforce HTTPS in all environments and add security headers middleware.
- Rate limiting / throttling for sensitive endpoints.

### Tooling & Dev Experience
- Add `dotnet format` / analyzers and enforce StyleCop or similar.
- Add GitHub Actions workflow for build + test + code coverage.
- Provide example REST client file (`.http` or `.rest`) with CRUD requests.

### Frontend Client
- Scaffold an Angular (or alternative) frontend calling the Web API.
- Add a generated TypeScript client (e.g., NSwag or OpenAPI generator) for strong typing.

## Contributing Workflow (Suggested)
1. Create a branch per enhancement (e.g., `feature/product-crud`).
2. Use Copilot to draft changes; review diffs locally.
3. Run tests (`dotnet test`).
4. Submit PR referencing the enhancement bullet.

## Quick Commands
```bash
dotnet build
dotnet test
dotnet run --project CoreWebApi
```

## License
Internal training project (no external license specified). Add a LICENSE file if distribution is intended.