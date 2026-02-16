# 2026 PraPDBL Siperuka 2026 - Backend

Siperuka 2026 is a Room Booking Information System for university campuses. This backend provides a RESTful API built with .NET 8, supporting room booking management, user management, booking history, and more.

## Main Features

- Room Booking CRUD
- Booking Status Management
- Room Management
- User Management
- Booking History
- Authentication & Authorization

## Folder Structure

- `Controllers/` — API Controllers
- `Models/` — Data models/entities
- `DTOs/` — Data Transfer Objects
- `Services/` — Business logic & service layer
- `Data/` — Database context, configuration, and seeders
- `Migrations/` — Entity Framework migration files
- `Converters/` — Data type converters
- `Properties/` — Project configuration

## Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/shintiaadwim/2026-siperuka-backend.git
   cd 2026-siperuka-backend/Backend
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Update the database (migrations)**

   ```bash
   dotnet ef database update
   ```

4. **Run the application**

   ```bash
   dotnet run
   ```

5. **API available at:**  
   `https://localhost:5001` or `http://localhost:5000`

## Configuration

- Configuration files: `appsettings.json`, `appsettings.Development.json`
- Make sure the database connection string is set correctly.

## API Documentation

- Main endpoints are available in each controller.
- Use tools like Postman or Swagger to explore the endpoints.

## Contribution

1. Fork & clone the repository
2. Create a new branch from `develop`
3. Commit your changes & push to your branch
4. Create a Pull Request to `develop`

## License

MIT License
