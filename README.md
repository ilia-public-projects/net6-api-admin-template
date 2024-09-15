# .NET 6 API Admin Template

![.NET](https://img.shields.io/badge/.NET-6.0-blue) ![License](https://img.shields.io/badge/license-MIT-green) ![EF Core](https://img.shields.io/badge/EF%20Core-6.0-orange)

## Overview

**net6-api-admin-template** is a backend API built with **.NET 6** and **Entity Framework Core**. This project provides a comprehensive template for building administrative dashboards or user management systems. It integrates with **Azure Blob Storage**, implements **custom authentication**, supports **SignalR** for real-time updates, and includes user management functionality.

This project serves as the backend for the [Metronic 8 Angular 16 UI](https://github.com/ilia-public-projects/metronic8-angular16-ui), offering a powerful solution for admin panels or any system requiring robust user and data management.

---

## Features

- **.NET 6 API**: Built with .NET 6, offering performance improvements and long-term support.
- **Entity Framework Core**: Uses EF Core for data access and manipulation.
- **Azure Blob Storage Integration**: Includes an example of integrating Azure Blob Storage for file storage and retrieval.
- **Custom Authentication**: Implements a custom authentication system for managing user logins and sessions.
- **SignalR**: Provides real-time updates using SignalR for WebSockets.
- **User Management**: Full user management system, including role-based access control.

---

## Technology Stack

- **Backend**:
  - .NET 6
  - ASP.NET Core
  - Entity Framework Core (EF Core)
- **Database**: 
  - SQL Server (or compatible databases)
- **Azure Integration**:
  - Azure Blob Storage
- **Authentication**:
  - Custom authentication with JWT (JSON Web Tokens)
- **Real-time Communication**:
  - SignalR for WebSockets
- **User Management**:
  - Role-based access control (RBAC)
  
---

## Prerequisites

Ensure you have the following installed on your system:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (or other compatible databases)
- An Azure account for Blob Storage integration (optional)

---

## Installation and Setup

### Steps to Run Locally

1. **Clone the repository**:
    ```bash
    git clone https://github.com/ilia-public-projects/net6-api-admin-template.git
    cd net6-api-admin-template
    ```

2. **Set Up Database**:
   Update your connection string in the `appsettings.json` file for Entity Framework Core to connect to your local or cloud SQL Server instance:
   
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=your-database;User Id=your-username;Password=your-password;"
     }
   }

