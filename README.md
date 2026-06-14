# movie-rental-system

A web application for managing a video rental store built with ASP.NET Core MVC.

## Features

- Browse and manage movie inventory
- Add and manage customer records
- Create rentals and process returns
- Automatic price calculation based on rental duration
- Real-time table search

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) (v10 or higher)
- Entity Framework Core tools:
```
dotnet tool install --global dotnet-ef
```

## How to run

1. Clone the repository:

```
git clone https://github.com/kondi7/movie-rental-system.git
```

2. Navigate to the project folder:

```
cd movie-rental-system
```

3. Apply database migrations:

```
dotnet ef database update
```

4. Run the application:

```
dotnet run
```

5. Open in browser: `http://localhost:5210`

## Technologies

- ASP.NET Core MVC — web framework
- Entity Framework Core — ORM / database access
- SQLite — database
- Razor Views — templating
- Bootstrap — UI styling
- JavaScript — table search, filtering
