# Sellify

[![.NET 10](https://img.shields.io/badge/.NET-10-512bd4?logo=dotnet)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server/)
[![Stripe](https://img.shields.io/badge/Stripe-635BFF?logo=stripe&logoColor=white)](https://stripe.com/)

**Sellify** is a high-performance, secure e-commerce platform designed for modern marketplaces. This project demonstrates a deep understanding of the .NET ecosystem, focusing on architectural integrity, secure authentication flows, and scalable data management.

---

## 🏗️ Architectural Overview

The project is built using **Clean Architecture** and follows the **CQRS (Command Query Responsibility Segregation)** pattern to ensure a clear separation between read and write operations.

* **Result Pattern:** Implemented for a unified and predictable error-handling strategy across the API.
* **Hybrid ORM Approach:** * **EF Core:** Used for 90% of the application to leverage robust change tracking and relationship management.
    * **Dapper:** Utilized specifically for fetching products on the landing page to achieve maximum performance and minimal latency.
* **Domain-Driven Design (DDD) Principles:** Focus on core business logic and entities.

---

## 🛡️ Security & Authentication

I’ve implemented a multi-layered security strategy to protect user data and financial transactions:

* **JWT Authentication:** Secure token-based access.
* **Secure Storage:** Tokens are delivered via **HttpOnly Cookies** to prevent XSS attacks.
* **Refresh Tokens:** Implemented to maintain user sessions securely without frequent re-logins.
* **Stripe Integration:** All payment processing is handled through Stripe API (Payment Intents & Checkout Sessions), ensuring PCI compliance.

---

## ✨ Key Features

* **Marketplace Logic:** Users can register as both buyers and sellers.
* **Inventory Management:** Sellers can add new products, upload images to the server, and edit existing listings.
* **Commerce Tools:** * Integrated Wishlist for authenticated users.
    * Product Review and Rating system.
    * Pagination and advanced filtering for product discovery.
* **Payment Gateway:** Fully functional Stripe integration for secure checkout.

---

## 🧪 Testing & Quality Assurance

Reliability is ensured through a comprehensive suite of unit tests:

* **Framework:** xUnit
* **Mocking:** Moq for isolating dependencies.
* **Assertions:** Fluent Assertions for expressive and readable test logic.

---

## 🚀 Getting Started

### Prerequisites
* **.NET 10 SDK**
* **SQL Server**
* **Stripe API Keys**

### Setup
1. **Clone the repository:**
   ```bash
   git clone https://github.com/mohamashraf4dv/Sellify-ASP.NET-API.git

2. **Configure Environment:**
   Open `appsettings.json` in the Web API project and update it with your credentials. 
   
   > ⚠️ **Note:** For production or public repositories, never commit your actual Stripe Secret Keys. Use Environment Variables or User Secrets for local development.

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=SellifyDB;Trusted_Connection=True;TrustServerCertificate=True;"
     },
     "Stripe": {
       "SecretKey": "sk_test_your_secret_key",
       "PublishableKey": "pk_test_your_publishable_key"
     }
   }

  3. Update-Database having migrations already set up
