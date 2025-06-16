# Incepta – API Gateway & Policy Engine

Incepta is a high-performance, extensible API Gateway built with ASP.NET Core and YARP. It enables secure and dynamic request routing across multi-tenant architectures, featuring built-in JWT authentication, IP blocking, rate limiting, and fine-grained policy-based authorization.

---

## 🚀 Features

- **🔐 JWT Token Validation**
- **🏢 Tenant-based Host Routing**
- **🧠 Policy-Based Authorization**
  - e.g., `Invoice.Add`, `Product.Delete`
- **📉 Rate Limiting**
  - Redis-backed IP+Route limiter
- **⛔ IP Blocking**
  - Persistent or temporary blacklisting
- **🔄 Dynamic Reverse Proxy Routing** (via YARP)
- **🛠 Admin API** for managing:
  - Tenants
  - Routes
  - Policies
  - IP Blocks
- **📦 Modular, Clean Architecture (DDD-inspired)**

---

## 📁 Project Structure

/Incepta.sln
/src
  /Incepta.ApiGateway         # Reverse proxy entry (YARP + Middleware)
  /Incepta.AdminApi           # Admin REST API (CRUD for tenants/routes/policies/IPs)
  /Incepta.Application        # Business logic (CQRS handlers)
  /Incepta.Domain             # Entities (Route, Tenant, Policy, IPBlock)
  /Incepta.Infrastructure
    /Jwt                      # JWT token validator
    /Redis                    # Rate limiter, IP block caching
    /Middleware               # Core gateway behaviors
  /Incepta.Persistence
    /Contexts                 # EF Core DB Context
    /Seed                     # DB Seeding logic
  /Incepta.Shared             # Common helpers / abstractions

/tests
  /Incepta.UnitTests
  /Incepta.IntegrationTests

/docker
  docker-compose.yml          # Redis + Gateway orchestration
  gateway.Dockerfile

/docs
  route-config.schema.json    # JSON schema for dynamic route config
  policy-config.schema.json   # Policy definition schema



🧪 Tech Stack

    ASP.NET Core 8

    YARP (Yet Another Reverse Proxy)

    Entity Framework Core (PostgreSQL)

    Redis for rate limiting

    FluentValidation

    MediatR for CQRS patterns

    StackExchange.Redis

    Docker for deployment

    Swagger (Admin API)

⚙️ Getting Started
Prerequisites

    .NET 8 SDK

    Docker

    PostgreSQL (local or containerized)

    Redis

🔧 Configuration

Update appsettings.json in both projects:

{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=InceptaDb;Username=postgres;Password=yourpassword",
    "Redis": "localhost:6379"
  }
}

🐳 Run with Docker

docker-compose up --build

Runs:

    Incepta.ApiGateway on http://localhost:5000

    Redis

🧑‍💻 Run Locally

# Run Gateway
cd src/Incepta.ApiGateway
dotnet run

# Run Admin API
cd ../Incepta.AdminApi
dotnet run

🔐 Example Admin API Endpoints
Entity	Method	URL
Tenant	GET	/api/tenants
Policy	POST	/api/policies
Route	PUT	/api/routes/{id}
IP Block	DELETE	/api/ipblocks/{id}
✨ Planned Features

Web UI Panel (Blazor or React)

Open Policy Agent (OPA) integration

Real-time metrics dashboard

    Webhook triggers on policy breach

🧑‍💼 Author

Tunahan Ali Öztürk
Senior .NET Backend Developer
🔗 LinkedIn
📄 License

MIT License — use freely, contribute openly.
