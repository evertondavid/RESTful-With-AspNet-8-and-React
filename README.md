# ASP.NET Core API Restful with React

## Project Overview

This project is a RESTful API developed using ASP.NET Core, designed to showcase best practices in building scalable web APIs. It features a MySQL database, utilizes ASP.NET Versioning, Evolve for database migrations, and Serilog for logging. This API is fully equipped with Swagger for API documentation, supports XML format, HATEOAS, CORS, and JWT for authentication. Enhancements such as GitHub Actions integration, and multi-database compatibility are currently in progress.

### Key Features:

- **Database Integration**: MySQL v5 with Evolve for migration management.
- **Error Logging**: Integrated Serilog for sophisticated runtime logging.
- **API Documentation**: Swagger UI for API exploration and testing at `http://localhost:44300/swagger/index.html`.
- **Versioning**: Endpoint version management to support backward compatibility.
- **Security**: Implements JWT-based authentication to secure endpoints.
- **Advanced Query Capabilities**: Supports query parameters and paged search for effective data fetching.
- **File Handling**: Handling file uploads and downloads (in progress).

## Technologies

- **ASP.NET Core**: For creating server-side logic
- **Entity Framework**: ORM used for database operations
- **MySQL**: Database management
- **ASP.NET Versioning.Mvc**: For API version control
- **Serilog**: For logging
- **Swagger**: API documentation and exploration tool
- **React.js**: Frontend framework (in progress)

## Architecture

This project follows a layered architecture:

- **Client**: Frontend consuming the API (React.js in progress)
- **Controller**: Handles incoming HTTP requests and responses
- **Business**: Business logic implementation
- **Repository**: Data access layer using generic repositories
- **Model**: Data models and entities

## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

- .NET 6 SDK
- MySQL Server
- Node.js and npm (for React.js development)

### How to Run the Project

1. **Clone the repository:**
   ```bash
   git clone https://github.com/evertondavid/RESTful-With-AspNet-7-and-React.git

2. **Start the application:**
   Copy code: dotnet run

   Open your browser and navigate to http://localhost:44300/swagger/index.html to view the application.

### Contributing
   Contributions are welcome! Please fork this repository and submit pull requests with any improvements.

### License
   This project is licensed under the MIT License - see the LICENSE file for details.

