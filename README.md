# Loan API

A robust ASP.NET Core Web API for managing loans and user accounts with role-based access control.

## 🌟 Features

### Authentication & Authorization
- JWT-based authentication
- Role-based access control (User and Accountant roles)
- Password hashing for security

### Loan Management
- Create, read, update, and delete loan applications
- Multiple loan types (FastLoan, AutoLoan, Installment)
- Loan status tracking (InProcess, Approved, Rejected)
- Currency support with validation
- Loan period validation (1-360 months)

### User Management
- User registration and profile management
- User blocking/unblocking functionality
- Monthly income tracking
- Age verification (minimum 18 years)

### Security & Validation
- Fluent Validation for all DTOs
- Exception handling middleware
- Request logging
- Custom exception types

### Technical Features
- Clean Architecture
- Repository pattern
- Auto-mapping with AutoMapper
- Entity Framework Core
- Multiple database contexts (User and Loan)
- Swagger/OpenAPI documentation

## 🏗️ Architecture

The solution follows Clean Architecture principles and is organized into four main projects:

```
LoanAPI.sln
├── Domain        (Enterprise business rules)
├── Application   (Application business rules)
├── Infrastructure(Frameworks and drivers)
└── API           (Interface adapters)
```

## 🚀 Getting Started

### Prerequisites
- .NET 6.0 SDK or later
- SQL Server
- Visual Studio 2022 or preferred IDE

### Installation

1. Clone the repository
```bash
git clone https://github.com/yourusername/loan-api.git
```

2. Update connection strings in `appsettings.json`
```json
{
  "ConnectionStrings": {
    "UserDatabase": "your_user_db_connection_string",
    "LoanDatabase": "your_loan_db_connection_string"
  }
}
```

3. Apply database migrations
```bash
dotnet ef database update --context UserDbContext
dotnet ef database update --context LoanDbContext
```

4. Run the application
```bash
dotnet run --project src/LoanAPI.API
```

## 🔑 API Endpoints

### Accountant Controller (Requires Accountant Role)
- GET `/api/accountant/loans` - Get all loans
- PUT `/api/accountant/loans/{id}/status` - Update loan status
- POST `/api/accountant/users/{userId}/block` - Block user
- POST `/api/accountant/users/{userId}/unblock` - Unblock user

### Loan Controller
- GET `/api/loan/{id}` - Get loan by ID
- GET `/api/loan/user/{userId}` - Get user's loans
- POST `/api/loan` - Create new loan
- PUT `/api/loan/{id}` - Update loan
- DELETE `/api/loan/{id}` - Delete loan

### User Controller
- GET `/api/user/{id}` - Get user by ID
- POST `/api/user` - Create user (Accountant only)
- PUT `/api/user/{id}` - Update user
- DELETE `/api/user/{id}` - Delete user (Accountant only)

## 🔒 Security

### JWT Configuration
Update the JWT settings in `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "your_secret_key",
    "Issuer": "your_issuer",
    "Audience": "your_audience",
    "ExpiryInHours": "24"
  }
}
```

### Password Requirements
- Minimum length: 6 characters
- Must contain at least one uppercase letter
- Must contain at least one lowercase letter
- Must contain at least one number

## 📝 Logging

The application uses Serilog for logging with the following sinks:
- Console logging
- SQL Server logging (automatic table creation)

## 🧪 Testing

The solution includes both unit tests:

```bash
dotnet test LoanAPI.UnitTests
```
