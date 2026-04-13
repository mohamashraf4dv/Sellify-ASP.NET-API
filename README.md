Sellify | Full-Stack E-Commerce API
Sellify is a robust, secure, and scalable e-commerce Web API built with .NET 10. It provides a complete marketplace experience where users can buy and sell items, managed through a modern architectural stack and secure payment processing.

🚀 Key Features
Secure Authentication: JWT-based auth using HttpOnly Cookies and Refresh Token rotation for maximum security.

Payment Integration: Fully integrated with Stripe API for secure checkout sessions and payment intents.

Performance Optimized: Hybrid ORM approach—EF Core for write operations and Dapper for lightning-fast product retrieval on the landing page.

Seller Dashboard: Dedicated tools for sellers to upload images, manage inventory, and track listings.

Customer Experience: Wishlist management, product reviews, and advanced pagination.

File Management: Built-in server-side file handling for product image uploads.

🏗️ Technical Architecture
This project follows Clean Architecture principles to ensure separation of concerns and maintainability:

Domain: Entities, Enums, and Core Logic.

Application: CQRS (Command Query Responsibility Segregation) pattern with MediatR.

Infrastructure: Persistence layer (SQL Server), Identity, and Third-party services (Stripe).

API: Controllers and Middleware using the Result Pattern for unified, predictable error handling.

🛠️ Tech Stack
Backend: .NET 10 Web API

Database: SQL Server

ORMs: EF Core (90%) & Dapper (High-speed reads)

Testing: xUnit, Moq, and Fluent Assertions

Security: JWT, Refresh Tokens, HttpOnly Cookies

Payments: Stripe SDK

🚦 Getting Started
Prerequisites
.NET 10 SDK

SQL Server

Stripe Account (for API Keys)

Installation
Clone the repository:

Bash
git clone https://github.com/your-username/sellify.git
Configure Environment: Update appsettings.json with your credentials:

JSON
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=SellifyDB;..."
  },
  "Stripe": {
    "SecretKey": "your_stripe_secret_key"
  }
}
Run Migrations:

Bash
dotnet ef database update
Launch:

Bash
dotnet run
🧪 Quality Assurance
The project includes a comprehensive test suite to ensure business logic reliability:

Unit Testing: Logic validated via xUnit.

Mocking: External dependencies handled through Moq.

Readability: Assertions written with Fluent Assertions for human-readable test cases.
