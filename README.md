# Clean Architecture Kit 🏗️

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)

A robust, production-ready .NET Web API starter template implementing Clean Architecture principles with modern best practices and essential features for enterprise applications.

## 🌟 Key Features

- **Clean Architecture Implementation**
  - Strict separation of concerns with layered architecture
  - Domain-driven design principles
  - SOLID principles adherence
  - Modular and maintainable codebase

- **Authentication & Security**
  - JWT-based authentication
  - Token blacklist implementation
  - Custom token validation
  - Password policy enforcement
  - Response security headers
  - IP whitelist support
  - Encoding/decoding utilities

- **Database & Data Access**
  - Entity Framework Core with Code First approach
  - Unit of Work pattern
  - Generic CRUD operations
  - Audit trail implementation
  - Database health monitoring

- **API Features**
  - API versioning
  - Request/Response logging with Watchdog
  - Global exception handling
  - Localization support
  - Generic pagination
  - MiniProfiler for query analysis
  - Health checks for service monitoring

- **Development Tools**
  - MediatR for CQRS pattern
  - FluentValidation for request validation
  - AutoMapper for object mapping
  - Serilog for structured logging
  - Scrutor for automated dependency injection
  - EditorConfig for consistent code style

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- PostgreSQL (or your preferred database)
- Visual Studio 2022 or preferred IDE

### Installation

1. Clone the repository
   ```bash
   git clone https://github.com/yourusername/CleanArchitectureKit.git
   ```

2. Navigate to the project directory
   ```bash
   cd CleanArchitectureKit
   ```

3. Restore dependencies
   ```bash
   dotnet restore
   ```

4. Update the connection string in `appsettings.json`

5. Apply database migrations
   ```bash
   dotnet ef database update
   ```

6. Run the application
   ```bash
   dotnet run --project WebApi
   ```

## 🏗️ Project Structure

```
CleanArchitectureKit/
├── Domain/           # Enterprise business rules
├── Application/      # Application business rules
├── Infrastructure/   # External concerns
├── WebApi/          # Entry point and configuration
├── Presentation/    # UI layer
└── UnitTest/        # Test projects
```

## 📚 Documentation

- API documentation is available at `/swagger` when running the application
- Detailed documentation can be found in the [Wiki](wiki-link)

## 🔍 Health Monitoring

- Health check endpoint: `/health`
- MiniProfiler UI: `/profiler/results-index`

## 🧪 Testing

Run the tests using:
```bash
dotnet test
```

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 🙏 Acknowledgments

- Clean Architecture by Robert C. Martin
- Microsoft .NET Team
- All contributors to this project

---

⭐ If you find this template useful, please consider giving it a star!
