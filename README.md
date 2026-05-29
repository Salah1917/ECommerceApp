# ECommerceApp

A full-featured **e-commerce backend API** built with ASP.NET Core 8.0 following **Clean Architecture (Onion Architecture)** principles. Provides RESTful endpoints for managing products, shopping baskets, user authentication, order processing, and Stripe payment integration.

## Tech Stack

- **Language:** C# (.NET 8.0)
- **Framework:** ASP.NET Core 8.0 Web API
- **ORM:** Entity Framework Core 8.0
- **Databases:** SQL Server (relational) + Redis (basket caching)
- **Authentication:** ASP.NET Core Identity + JWT Bearer tokens
- **Payments:** Stripe.net
- **Object Mapping:** AutoMapper
- **API Docs:** Swagger / Swashbuckle

## Project Structure

```
ECommerceApp.sln
├── ECommerceApp/                 # Web API startup project (entry point)
│   ├── Program.cs                # App builder, DI, middleware pipeline
│   ├── appsettings.json          # Configuration (DB, JWT, Stripe, Redis)
│   ├── Middlewares/
│   └── wwwroot/images/products/  # Static product images
├── Core/
│   ├── DomainLayer/              # Entities, enums, repository contracts
│   ├── ServiceAbstraction/       # Service interfaces
│   └── Service/                  # Service implementations + specifications
├── Infrastructure/
│   ├── Presistance/              # EF DbContext, repositories, migrations, seed data
│   └── Presentation/             # API Controllers
└── Shared/                       # DTOs, error models, pagination
```

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server instance
- Redis instance
- Stripe account (for payments)

## Setup

1. **Clone the repository:**
   ```bash
   git clone <repo-url>
   cd ECommerceApp
   ```

2. **Configure secrets** in `ECommerceApp/appsettings.json`:
   - `ConnectionStrings:DefaultConnection` — SQL Server (store)
   - `ConnectionStrings:IdentityConnection` — SQL Server (identity)
   - `ConnectionStrings:Redis` — Redis connection (default: `localhost`)
   - `JWT:AuthKey` — Secret key for JWT signing
   - `StripeSettings:Publishablekey` / `StripeSettings:Secretkey` — Stripe API keys

3. **Restore and build:**
   ```bash
   dotnet restore
   dotnet build
   ```

4. **Run the application:**
   ```bash
   dotnet run --project ECommerceApp
   ```
   Migrations and seed data apply automatically on startup.

5. **Swagger UI:**
   - HTTPS: `https://localhost:7223/swagger`
   - HTTP: `http://localhost:5179/swagger`

## API Endpoints

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| GET | `/api/product` | No | List products (pagination, filter, sort) |
| GET | `/api/product/{id}` | No | Get product by ID |
| GET | `/api/product/Types` | No | List product types |
| GET | `/api/product/Brands` | No | List product brands |
| GET/POST/DELETE | `/api/basket` | No | Manage shopping basket (Redis) |
| POST | `/api/account/login` | No | Login, returns JWT |
| POST | `/api/account/register` | No | Register, returns JWT |
| GET | `/api/account` | Yes | Get current user |
| GET/PUT | `/api/account/address` | Yes | Get/update user address |
| POST | `/api/orders` | Yes | Create order from basket |
| GET | `/api/orders` | Yes | Get user orders |
| GET | `/api/orders/{id}` | Yes | Get order by ID |
| GET | `/api/orders/deliveryMethods` | Yes | List delivery methods |
| GET | `/api/payment/{basketId}` | Yes | Create/update Stripe PaymentIntent |

## Architecture Highlights

- **Clean Architecture** — 4-layer separation: Domain → Service → Infrastructure → Presentation
- **Repository + Unit of Work** — Generic repository with specification pattern support
- **Redis Basket** — Shopping baskets stored in Redis for fast, temporary storage
- **Specification Pattern** — Encapsulates query logic (filtering, sorting, pagination, eager loading)
- **Global Error Handling** — Exception middleware catches unhandled errors
- **Stripe Integration** — Payment intents created/updated when baskets change
- **JWT Authentication** — Bearer tokens with configurable claims, issuer, and expiry

## Key Packages

- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `AutoMapper.Extensions.Microsoft.DependencyInjection`
- `StackExchange.Redis`
- `Stripe.net`
- `Swashbuckle.AspNetCore`
