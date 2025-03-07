# 📚 Project Title & Description 📚
**A modern web application designed to manage and interact with a collection of books and authors. Built using the Clean Architecture principles, this application leverages ASP.NET Core for the backend, providing a robust and scalable solution for users to register, log in, and manage their favorite books and authors.**



## ✨ Features 
- 🏷️ Manage books and authors
- 🔑 JWT-based authentication
- 👮 Role-based authorization
- 💖 Make own list of favorite authors and books 
- 📦 Caching with Redis for fast responses
- 📄 OpenAPI documentation with Scalar

# 📘Getting Started📘
## ✔️Technology Stack
> **Backend:**
- ASP.NET Core 9.0
> **Database:** 
- SQLite for data storage, with Entity Framework Core for ORM.
> **Caching:**
- Redis for caching frequently accessed data.
> **Dependency Injection:** 
- Built-in support for dependency injection, promoting loose coupling and easier testing.


## 🚀 Installation & Setup 
1. Clone the repository:
   ```sh
   git clone https://github.com/Tealinabeh/DemoCleanArchitectureProject
   cd DemoBookApp
2. Build and run with Docker:
   ```sh
   docker-compose up --build

## 📖 API Documentation
You can explore the API using Scalar:
- **Scalar UI**: [http://localhost:7070/scalar/v1](http://localhost:7070/scalar/v1)
