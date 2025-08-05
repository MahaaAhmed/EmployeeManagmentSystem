# 🧑‍💼 Employee Management System

This is a web-based **Employee Management System** built using **.NET 7 (ASP.NET Core)** following the **Onion Architecture** and implementing the **Repository Design Pattern**. The application allows users to manage employee data through full **CRUD operations** (Create, Read, Update, Delete).

Additionally, the system includes **real-time notifications** using **SignalR**, and **background job processing** using **Hangfire**.

---

## 🛠 Technologies & Frameworks Used

- **.NET 7** (ASP.NET Core)
- **Onion Architecture**
- **Entity Framework Core**
- **Repository Design Pattern**
- **SignalR** for real-time communication
- **Hangfire** for background jobs
- **SQL Server**
- **ASP.NET MVC** (for dashboard)
- **Data Annotations** and Fluent Validation (optional)
- **Custom Exception Handling**
- **Logging** (built-in)

---

## 🧩 Features

✅ Full CRUD operations for managing employees  
✅ Real-time notifications using SignalR  
✅ Background job running every hour using Hangfire  
✅ Inactive employees deactivated after 90 days of inactivity  
✅ Modular, testable project structure with Onion Architecture  
✅ MVC Dashboard interface for admins  
✅ Custom exception handling and error modeling  
✅ Model validation before processing  
✅ Scalable and maintainable codebase

---

## 🧱 Architecture Overview

The solution uses **Onion Architecture**, dividing the project into clear layers to ensure separation of concerns:
