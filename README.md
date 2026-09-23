# 📚 LexiTale – AI-Powered Language Learning Backend

LexiTale is a backend API for an AI-powered language learning application.
It helps users learn and practice vocabulary through personalized stories and AI-generated exercises.

The backend is built with **ASP.NET Core Web API** and follows a clean, modular architecture.

---

## 🚀 Features

* 🔐 User Registration & Login
* 🎫 JWT Authentication
* 🔄 Refresh Token Authentication
* 📖 Vocabulary Management
* 📦 Bulk Word Addition
* 🤖 AI-Powered Story Generation
* 📝 AI-Generated Comprehension Questions
* 🎯 AI-Powered Answer Evaluation
* 🗄️ SQL Server Database
* 🧩 Entity Framework Core
* 🛡️ Global Exception Handling
* 📑 Swagger / OpenAPI Documentation
* 🏗️ Clean Architecture

---

## 🛠️ Tech Stack

### Backend

* **C#**
* **ASP.NET Core Web API**
* **.NET 8**
* **Entity Framework Core**
* **SQL Server**
* **ASP.NET Core Identity**
* **JWT Authentication**

### AI

* **Google Gemini API**

### API Documentation

* **Swagger / OpenAPI**

### Architecture

* Clean Architecture
* Repository Pattern
* Dependency Injection
* Service Layer

---

## 🏗️ Project Structure

```text
LexiTale
│
├── LexiTale.API
│   ├── Controllers
│   ├── Middlewares
│   ├── Extensions
│   └── Program.cs
│
├── LexiTale.Application
│   ├── DTOs
│   ├── Interfaces
│   └── Services
│
├── LexiTale.Domain
│   ├── Entities
│   ├── Enums
│   └── Interfaces
│
└── LexiTale.Persistence
    ├── Context
    ├── Configurations
    ├── Repositories
    └── Migrations
```

---

## 🔑 Authentication

LexiTale uses **JWT Bearer Authentication**.

### Authentication Flow

```text
Register
   ↓
Login
   ↓
Access Token
   ↓
Authorized API Requests
   ↓
Refresh Token
   ↓
New Access Token
```

Protected endpoints require:

```text
Authorization: Bearer {access_token}
```

---

## 📖 Vocabulary Management

Authenticated users can manage their personal vocabulary.

Available operations include:

```text
POST   /api/words
GET    /api/words
GET    /api/words/{id}
PUT    /api/words/{id}
DELETE /api/words/{id}
POST   /api/words/bulk
```

Each user's vocabulary is isolated from other users.

---

## 🤖 AI Story Generation

LexiTale integrates with **Google Gemini** to create personalized learning stories.

The application uses the user's vocabulary and selected learning level to generate a story with comprehension questions.

### Example Flow

```text
User Vocabulary
      ↓
Select Learning Level
      ↓
Generate Story
      ↓
Gemini AI
      ↓
Story + Questions
```

---

## 🎯 AI Answer Evaluation

After reading the generated story, users can answer the questions.

The backend sends the answers to Gemini for evaluation and returns feedback based on the user's responses.

```text
Story
  ↓
Questions
  ↓
User Answers
  ↓
AI Evaluation
  ↓
Score + Feedback
```

---

## 🗄️ Database

LexiTale uses **SQL Server** with **Entity Framework Core**.

Main entities include:

* Users
* Words
* Exercises
* Stories

Entity relationships and database schema are managed using EF Core migrations.

---

## ⚙️ Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/LexiTale.git
cd LexiTale
```

### 2. Configure the Database

Update the connection string in your configuration.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=LexiTaleDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

### 3. Configure JWT and Gemini

Do **not** commit real API keys or secrets to GitHub.

Use **User Secrets** during development or environment variables in production.

Example:

```json
{
  "Jwt": {
    "Key": "YOUR_JWT_SECRET",
    "Issuer": "LexiTale",
    "Audience": "LexiTaleUsers"
  },
  "Gemini": {
    "ApiKey": "YOUR_GEMINI_API_KEY"
  }
}
```

---

### 4. Apply Database Migrations

From Visual Studio Package Manager Console:

```powershell
Update-Database
```

Make sure the Default Project is:

```text
LexiTale.Persistence
```

---

### 5. Run the API

Run the project from Visual Studio.

Swagger will be available at the configured HTTPS URL:

```text
/swagger
```

Swagger provides an interactive interface for testing the API endpoints.

---

## 🧪 API Testing

The complete application flow can be tested through Swagger:

```text
Register
   ↓
Login
   ↓
Authorize
   ↓
Add Vocabulary
   ↓
Get Vocabulary
   ↓
Generate Story
   ↓
Answer Questions
   ↓
AI Evaluation
```

---

## 🔒 Security

The project uses:

* JWT Bearer Authentication
* Password hashing through ASP.NET Core Identity
* User-specific data access
* Authorization for protected endpoints
* Secure configuration for API keys and JWT secrets

**Never commit real secrets or API keys to the repository.**

---

## 📌 Project Status

```text
Backend Development: ✅ Completed
Authentication:      ✅ Completed
Vocabulary API:      ✅ Completed
AI Story Generation: ✅ Completed
AI Evaluation:       ✅ Completed
Database:            ✅ Completed
Swagger:             ✅ Completed
```

---



## 📄 License

This project was developed as an educational/software engineering project.
