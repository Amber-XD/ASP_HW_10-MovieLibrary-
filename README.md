# Movie Library

A full-stack web application for managing a personal movie collection. Users can register, log in, view movies, add new movies, and remove movies from their collection.

## Technologies

### Backend
- C#
- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- ASP.NET Core Identity
- JWT Authentication
- AutoMapper
- FluentValidation
- Swagger

### Frontend
- React
- JavaScript
- CSS
- Fetch API
- Vite

## Project Structure

```text
MovieLibrary/
│
├── Bll/          # Business Logic Layer
├── DLL/          # Data Access Layer
├── Domain/       # Domain models
├── UI/           # ASP.NET Core Web API
│
├── client/       # React frontend
│   ├── src/
│   │   ├── components/
│   │   ├── services/
│   │   ├── App.jsx
│   │   └── App.css
│   └── package.json
│
└── UI.slnx
```

## Features

- User registration and login
- JWT-based authentication
- Secure API endpoints
- Displaying movies from the database
- Adding new movies
- Removing movies from the collection
- Movie information, including title, description, genre, director, release date, country, and poster
- Data persistence using SQL Server
- Input validation and error handling

## Requirements

Before running the project, make sure you have installed:

- .NET SDK
- Node.js and npm
- Microsoft SQL Server
- Visual Studio or another C# IDE

## Installation and Setup

### 1. Clone the repository

```bash
git clone YOUR_REPOSITORY_URL
cd MovieLibrary
```

### 2. Configure the database

Open the backend project and configure the SQL Server connection string in `UI/appsettings.json`.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  }
}
```

Make sure the connection string matches the one used in your application's DbContext configuration.

### 3. Apply database migrations

Open the Package Manager Console in Visual Studio and select the appropriate project containing your migrations.

Run:

```powershell
Update-Database
```

### 4. Configure JWT

Add your JWT configuration to `UI/appsettings.json` according to the settings used by the application.

Do not commit real passwords, secret keys, or production credentials to GitHub.

### 5. Run the backend

Set `UI` as the startup project in Visual Studio and run the application.

The API and Swagger documentation will be available at the configured HTTPS address.

### 6. Run the frontend

Open a terminal in the `client` directory:

```bash
cd client
npm install
npm run dev
```

Open the local URL displayed by Vite in your browser.

## API Functionality

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Auth/register` | Register a new user |
| POST | `/api/Auth/login` | Log in |
| GET | `/api/Movie` | Get user's movies |
| POST | `/api/Movie` | Add a movie |
| DELETE | `/api/Movie/{id}` | Remove a movie |

Movie endpoints require JWT authentication.

## Authentication

After a successful login, the backend returns an access token.

The frontend uses this token in the `Authorization` header:

```http
Authorization: Bearer YOUR_ACCESS_TOKEN
```

## Author

Created as a full-stack web development project.
