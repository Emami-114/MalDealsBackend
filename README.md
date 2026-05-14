# 🛍️ MalDeals Backend

The backend for the **MalDeals** platform — a deal & bargain community where users can share, rate, and manage offers.

Built with **.NET 9**, **PostgreSQL**, **MinIO**, and **Docker** — production-ready with automated CI/CD via GitHub Actions.

---

## ✨ Features

| Feature | Description |
|---|---|
| 🔐 **JWT Authentication** | Secure token-based auth with configurable expiry |
| 🔑 **API Key Middleware** | Every request is validated via an HMAC-signed API key |
| 📦 **Deal Management** | Full CRUD for deals with price, offer price, categories, tags, coupon codes |
| 🗳️ **Deal Voting** | Users can upvote or downvote deals |
| 🛒 **Market Deals** | Personal marketplace lists per user |
| 🗂️ **Categories & Tags** | Flexible categorization and tagging of deals |
| 🏪 **Provider Management** | Manage shops and deal providers |
| 🖼️ **Image Upload** | Image upload & management via MinIO (S3-compatible) |
| 👤 **User Management** | Registration, login, profile management |
| 📖 **Swagger & Scalar UI** | Interactive API documentation in the browser |
| 🐳 **Docker Support** | Dev and prod Compose configurations included |
| ⚡ **CI/CD** | GitHub Actions: Build → Test → Docker Push → Deploy to VPS |

---

## 🧰 Tech Stack

| Technology | Version | Purpose |
|---|---|---|
| [.NET](https://dotnet.microsoft.com/) | 9.0 | Web framework (ASP.NET Core) |
| [PostgreSQL](https://www.postgresql.org/) | latest | Relational database |
| [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) | 9.0 | ORM & database migrations |
| [Npgsql](https://www.npgsql.org/) | 9.0.4 | PostgreSQL driver for EF Core |
| [MinIO](https://min.io/) | 6.0.4 | S3-compatible object storage for images |
| [JWT Bearer](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/) | 9.0.2 | Token authentication |
| [Swashbuckle (Swagger)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) | 7.3.1 | API documentation |
| [Scalar](https://scalar.com/) | 2.0.20 | Modern API reference UI |
| [dotenv.net](https://github.com/bolorundurowb/dotenv.net) | 3.2.1 | `.env` file loader |
| [Docker](https://www.docker.com/) | — | Containerization |
| [Caddy](https://caddyserver.com/) | latest | Reverse proxy + automatic TLS |
| [GitHub Actions](https://github.com/features/actions) | — | CI/CD pipeline |

---

## 📁 Project Structure

```
MalDealsBackend/
├── MalDealsBackend/
│   ├── Controllers/        # API endpoints (Deal, Auth, User, Vote, ...)
│   ├── Services/           # Business logic
│   ├── Models/
│   │   ├── Entitys/        # Database entities
│   │   └── DTOs/           # Data Transfer Objects
│   ├── Data/               # DbContext (EF Core)
│   ├── Middleware/         # API key validation, Swagger configuration
│   ├── Migrations/         # EF Core migrations
│   ├── Utils/              # Helper utilities
│   ├── Program.cs          # Application entry point
│   └── Dockerfile
├── docker-compose-dev.yml  # Local development (PostgreSQL + MinIO)
├── docker-compose-prod.yml # Production (App + DB + MinIO + Caddy)
├── Caddyfile               # Reverse proxy configuration
├── .github/workflows/      # CI/CD pipeline
└── .env                    # Environment variables (never commit!)
```

---

## 🚀 Quick Start (Local)

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) & Docker Compose
- (Optional) `dotnet-ef` CLI tool

### 1️⃣ Clone the repository

```sh
git clone https://github.com/Emami-114/MalDealsBackend.git
cd MalDealsBackend
```

### 2️⃣ Set up environment variables

```sh
cp .env.example .env
# Fill in your values in .env
```

### 3️⃣ Start infrastructure (PostgreSQL + MinIO)

```sh
docker compose -f docker-compose-dev.yml up -d
```

### 4️⃣ Install dependencies & apply migrations

```sh
dotnet restore
dotnet ef database update --project MalDealsBackend
```

### 5️⃣ Run the app

```sh
cd MalDealsBackend
dotnet run
# or with hot-reload:
dotnet watch run
```

### 6️⃣ Open API documentation

| UI | URL |
|---|---|
| 📖 Swagger | `http://localhost:5000/swagger` |
| 🚀 Scalar | `http://localhost:5000/scalar` |

---

## 🐳 Docker — Production

### Start the full production environment

```sh
# Fill .env with production values
cp .env.example .env

docker compose -f docker-compose-prod.yml up -d
```

Production includes:
- ✅ ASP.NET Core app (port 5051)
- ✅ PostgreSQL database
- ✅ MinIO object storage (port 9000 / console 9090)
- ✅ Caddy reverse proxy with automatic HTTPS

---

## 🔧 Makefile Commands

```sh
make run                    # Start the app
make watch                  # Start the app with hot-reload
make build                  # Build the project
make dbUpdate               # Reset DB and re-run migrations
make allContainerRemove     # Remove all Docker containers
make allImageRemove         # Remove all Docker images
make allDockerVolumeRemove  # Remove all Docker volumes
```

---

## 🔑 API Authentication

Every API request (except Swagger/Scalar) requires a valid API key in the header:

```
X-API-Key: <your-generated-api-key>
```

Protected endpoints additionally require a **JWT Bearer token**:

```
Authorization: Bearer <jwt-token>
```

---

## ⚙️ CI/CD Pipeline

Every push to `main` automatically triggers:

```
Push → main
  └─ Build & Test (.NET)
  └─ Build & push Docker image to Docker Hub
  └─ Deploy to VPS via SSH
       └─ Write .env on server
       └─ docker compose pull & up
```

**Required GitHub Secrets:**

| Secret | Description |
|---|---|
| `DATABASE_URL` | PostgreSQL connection string |
| `JWT_SECRET_KEY` | JWT signing key |
| `JWT_EXPIRY_MINUTES` | Token expiry time in minutes |
| `POSTGRES_USER` | Database username |
| `POSTGRES_PASSWORD` | Database password |
| `POSTGRES_DB` | Database name |
| `MINIO_ROOT_USER` | MinIO access key |
| `MINIO_ROOT_PASSWORD` | MinIO secret key |
| `MINIO_ENDPOINT_CONTAINER` | MinIO internal container endpoint |
| `API_KEY_SECRET_KEY` | API key HMAC secret |
| `DOCKER_USERNAME` | Docker Hub username |
| `DOCKER_PASSWORD` | Docker Hub password |
| `SERVER_HOST` | VPS IP address |
| `SERVER_USER` | VPS SSH user |
| `SERVER_PASSWORD` | VPS SSH password |
| `SSH_PRIVATE_KEY` | SSH private key |

---

## 📡 API Endpoints (Overview)

| Resource | Endpoints |
|---|---|
| 🔐 Auth | `POST /api/auth/register`, `POST /api/auth/login` |
| 👤 User | `GET /api/user`, `PUT /api/user`, `DELETE /api/user` |
| 🛍️ Deals | `GET /api/deal`, `POST /api/deal`, `PUT /api/deal/{id}`, `DELETE /api/deal/{id}` |
| 🗳️ Voting | `POST /api/dealvote`, `DELETE /api/dealvote` |
| 🛒 Market | `GET /api/marketdeal`, `POST /api/marketdeal` |
| 🗂️ Categories | `GET /api/category`, `POST /api/category` |
| 🏷️ Tags | `GET /api/tag`, `POST /api/tag` |
| 🏪 Providers | `GET /api/provider`, `POST /api/provider` |
| 🖼️ Images | `POST /api/image/upload`, `DELETE /api/image` |
| 🔑 API Key | `POST /api/apikey/generate` |

> Full documentation available at `/swagger` or `/scalar`

---

## 📄 License

MIT License — see [LICENSE](LICENSE)
