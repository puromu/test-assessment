# Assessment Exam System

Online examination system built with Angular and ASP.NET Core Web API.

---

# Features

- Online examination system
- Load questions from API
- Submit exam result
- Save results to PostgreSQL / Neon
- Responsive UI
- Validation handling
- Unit testing
- Docker support
- CI/CD with GitHub Actions

---

# Tech Stack

## Frontend
- Angular 21
- TypeScript
- HttpClient
- FormsModule

## Backend
- ASP.NET Core 9 Web API
- Entity Framework Core
- PostgreSQL / Neon
- Repository Pattern
- Layered Architecture

## Testing
- xUnit
- Moq

## CI/CD
- GitHub Actions

## Container
- Docker
- Docker Compose

---

# Architecture

```text
Angular
   ↓
ASP.NET Core API
   ↓
Application Service
   ↓
Repository
   ↓
PostgreSQL / Neon
```

---

# Project Structure

```text
Assessment/
│
├── frontend/
│   ├── src/
│   ├── Dockerfile
│   └── package.json
│
├── backend-api/
│   ├── Assessment.Api/
│   ├── Assessment.Application/
│   ├── Assessment.Domain/
│   ├── Assessment.Infrastructure/
│   └── Assessment.Tests/
│
├── database/
│   └── init.sql
│
├── .github/
│   └── workflows/
│       └── ci.yml
│
├── docker-compose.yml
├── .gitignore
└── README.md
```

---

# API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | /api/assessment/questions | Get questions |
| POST | /api/assessment/results | Submit exam result |
| GET | /api/assessment/results | Get exam results |

---

# Frontend Setup

```bash
cd frontend

npm install

ng serve
```

Open:

```text
http://localhost:4200/exam
```

---

# Backend Setup

Before running backend locally, configure database connection in:

```text
backend-api/Assessment.Api/appsettings.json
```

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your-neon-host;Port=5432;Database=your_db;Username=your_user;Password=your_password;SSL Mode=Require;Trust Server Certificate=true"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "AllowedHosts": "*"
}
```

Run backend:

```bash
cd backend-api/Assessment.Api

dotnet restore

dotnet run
```

Swagger:

```text
http://localhost:5209/swagger
http://localhost:5209/index.html
```


---

# Run Unit Test

```bash
cd backend-api

dotnet test
```

---

# Database Setup

Run SQL script:

```text
database/init.sql
```

on PostgreSQL / Neon database.

---

# Docker Setup

Before running Docker Compose, create `.env` file at project root.

Example:

```env
ASPNETCORE_ENVIRONMENT=Development

ConnectionStrings__DefaultConnection=Host=your-neon-host;Port=5432;Database=your_db;Username=your_user;Password=your_password;SSL Mode=Require;Trust Server Certificate=true
```

Build and run containers:

```bash
docker compose up --build
```

Frontend:

```text
http://localhost:4200/exam
```

Backend Swagger:

```text
http://localhost:5209/swagger
http://localhost:5209/index.html
```

---

# CI/CD

GitHub Actions automatically:

- Build frontend
- Build backend
- Run unit tests

on every push to `main`.

---

# Notes

Do not commit:

- `.env`
- real database credentials
- `appsettings.Development.json`