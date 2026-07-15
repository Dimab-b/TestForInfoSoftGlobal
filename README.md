# 🐶 Breeder Platform API

A RESTful ASP.NET Core Web API built as a technical assessment.

The project simulates a real-world business scenario for a breeder platform where verified breeders can publish litters after administrator approval. It follows Clean Architecture principles, CQRS, Domain-Driven Design concepts, and focuses on maintainability and scalability.

---

# Business Requirements

The API supports two core business operations:

- Publish an approved litter
- Get breeder litters with filtering and pagination

Publishing a litter includes business validation:

- Only the owner can publish
- Only Approved litters can be published
- Breeder has a limited number of free publications
- Every publish attempt is audited
- Successful publication sends a notification

---

# Tech Stack

- ASP.NET Core 8 Web API
- Entity Framework Core
- SQLite
- Dapper (Read Model)
- MediatR
- CQRS
- Clean Architecture
- Domain-Driven Design (DDD)
- Repository Pattern
- Unit of Work
- Domain Events
- Value Objects
- Dependency Injection
- Global Exception Handling
- ProblemDetails
- Seed Data

---

# Architecture

The solution follows Clean Architecture.

```
Presentation (Controllers)
        │
        ▼
Application (CQRS, MediatR)
        │
        ▼
Domain (Business Rules)
        │
        ▼
Infrastructure (EF Core, SQLite, Repositories)
```

Responsibilities are clearly separated.

- Controllers only receive HTTP requests.
- Business logic lives inside Application and Domain.
- Infrastructure contains persistence logic.

---

# Project Structure

```
src

├── Api
│   ├── Controllers
│   └── Middleware
│
├── Application
│   ├── Commands
│   ├── Queries
│   ├── DTOs
│   └── Events
│
├── Domain
│   ├── Entities
│   ├── Exceptions
│   ├── ValueObjects
│   └── Interfaces
│
└── Infrastructure
    ├── Persistence
    ├── Repositories
    └── Services
```

---

# Implemented Patterns

- Clean Architecture
- CQRS
- Repository Pattern
- Unit of Work
- Domain Events
- Dependency Injection
- Value Objects

---

# Business Rules

## Publish Litter

A breeder can publish a litter only if:

- the breeder owns the litter
- the litter status is Approved
- the breeder has available free publications

Successful publication:

- changes status to Published
- increments UsedCount
- creates AuditLog
- sends notification

Failed publication:

- keeps data unchanged
- writes AuditLog
- returns appropriate error

---

# Exception Handling

The API uses custom domain exceptions together with Global Exception Middleware.

Examples:

- LitterNotFoundException
- BreederNotFoundException
- InvalidLitterStatusException
- BenefitLimitException
- LitterNoOwnException

Errors are returned using RFC7807 ProblemDetails format.

---

# Read / Write Separation

Write operations are implemented using Entity Framework Core.

Read operations are implemented using Dapper.

This follows the CQRS principle where:

- Commands optimize consistency.
- Queries optimize performance.

---

# Domain Events

Successful publication raises a Domain Event.

```
Publish()

↓

LitterPublishedDomainEvent

↓

Notification Handler

↓

Email Notification Service
```

Business logic remains decoupled from infrastructure.

---

# API Endpoints

## Publish Litter

```
POST /api/litters/{litterId}/publish
```

Headers

```
X-Breeder-Id
```

---

## Get Litters

```
GET /api/litters
```

Query parameters

```
status
pageNumber
pageSize
```

---

# Seed Data

The application creates sample data automatically.

Test breeder:

```
Id:

11111111-1111-1111-1111-111111111111
```

Includes

- free publication limit
- approved litter
- submitted litter

---

# Running

Clone repository

```
git clone ...
```

Run

```
dotnet restore

dotnet run
```

---

# Example Response

```json
{
  "items": [],
  "totalCount": 2,
  "pageNumber": 1,
  "pageSize": 10
}
```

---

# Future Improvements (Enterprise-Level)

If this project were developed for production, I would additionally implement:

## Authentication & Authorization

- JWT Authentication
- Refresh Tokens
- Role-based Authorization
- Policy-based Authorization

---

## Validation

- FluentValidation
- MediatR Validation Pipeline

---

## Logging

- Serilog
- Structured Logging

---

## Caching

- Redis
- Distributed Cache

---

## Background Processing

- Hangfire

For asynchronous notifications, scheduled jobs and retries.

---

## Observability

- Health Checks

---

## Database

- PostgreSQL
- Optimistic Concurrency
- Database Migrations
- Index Optimization

---

## Security

- Rate Limiting
- API Versioning
- CORS Policies
- Secret Management

---

## CI/CD

- GitHub Actions
- Docker
- Docker Compose
- Automated Testing
- Automatic Deployment

---

## Testing

- Unit Tests
- Integration Tests
- TestContainers
- WebApplicationFactory

---

## API

- OpenAPI / Swagger
- API Versioning
- ProblemDetails
- Request Correlation IDs

---

# Design Decisions

This project intentionally separates:

- Business logic
- Persistence
- Infrastructure
- HTTP layer

to keep the solution maintainable, testable and scalable.

The architecture was designed to resemble a simplified enterprise backend rather than a CRUD application.
