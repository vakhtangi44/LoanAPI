# Loan Management API

This project is a Loan Management API designed with a robust architecture to handle user registration, loan management, and role-based access control. The API follows the principles of SOLID design and includes essential features like JWT authentication, logging, exception handling, and validation.

## Features

- **User Management**: Create, update, delete, and manage users.
- **Loan Management**: Create, update, delete, and fetch loan information.
- **Role-Based Access Control**:
  - Accountant: Manage loans and user accounts.
  - User: Manage their own loans.
- **Authentication & Authorization**: JWT-based authentication and role-based authorization.
- **Validation**: Input validation using FluentValidation.
- **Logging**: Integrated logging using Serilog.
- **Exception Handling**: Middleware for handling exceptions and saving them to the database.
- **Database**: Separate databases for user and loan management.

## Technologies Used

- **Language**: C#
- **Framework**: .NET 6
- **Authentication**: JWT (Json Web Tokens)
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Logging**: Serilog
- **Validation**: FluentValidation
- **API Documentation**: Swagger

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-repo-name.git
   cd LoanManagementAPI
   ```

2. **Set up the database**:
   Ensure SQL Server is installed and accessible.
   Update the connection strings in `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "UserDatabase": "Your_User_Database_Connection_String",
       "LoanDatabase": "Your_Loan_Database_Connection_String"
   }
   ```

3. **Run the application**:
   ```bash
   dotnet run
   ```

4. **Access the API**:
   Open a browser and navigate to [http://localhost:5000/swagger](http://localhost:5000/swagger) for Swagger API documentation.

## API Endpoints

### User Endpoints

- **GET** `/api/user/{id}`: Get user details.
- **POST** `/api/user`: Create a new user (Accountant only).
- **PUT** `/api/user/{id}`: Update user details.
- **DELETE** `/api/user/{id}`: Delete a user (Accountant only).

### Loan Endpoints

- **GET** `/api/loan/{id}`: Get loan details.
- **POST** `/api/loan`: Create a new loan.
- **PUT** `/api/loan/{id}`: Update a loan.
- **DELETE** `/api/loan/{id}`: Delete a loan.

### Accountant Endpoints

- **GET** `/api/accountant/loans`: Get all loans.
- **PUT** `/api/accountant/loans/{id}/status`: Update the status of a loan.
- **POST** `/api/accountant/users/{userId}/block`: Block a user.
- **POST** `/api/accountant/users/{userId}/unblock`: Unblock a user.

## Architecture

The project follows a 4-layer architecture:

1. **WebAPI**: Handles HTTP requests and responses.
2. **Application**: Contains business logic and DTOs.
3. **Domain**: Defines core entities, enums, and interfaces.
4. **Infrastructure**: Implements repositories, authentication, and logging.

## Security

- Passwords are hashed using SHA-256.
- JWT tokens are used for secure authentication and authorization.
- Database access is controlled using role-based authorization.

## Logging and Exception Handling

- Logs are stored in the database and output to the console.
- Unhandled exceptions are logged in the database for debugging.

## Future Enhancements

- Add unit and integration tests for all layers.
- Implement rate limiting to prevent abuse.
- Add support for additional loan types.

---

Feel free to contribute to the project by submitting pull requests or issues!
