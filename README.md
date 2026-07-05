# Hotel

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)

## General info

*Hotel* is an application that allows employees to manage the hotel operations and customers to book rooms. The project is currently under development.

## Technologies

### Backend:
* Microsoft SQL Server
* C#
* ASP.NET
* Entity Framework

### Frontend  (planned)
* React
* TypeScript
* HTML
* CSS

Project follows RESTful API principles and uses JWT-based authentication.

## Setup

### 1. Install required software

1.1. Install Microsoft SQL Server Express and select *Basic* version.

1.2. Install Microsoft Visual Studio (*Community* version) with workloads:
* ASP.NET and web development
* Data storage and processing

1.3. You can also install SQL Server Management Studio (no additional workloads or components needed) if you want to see the database.

### 2. Download ZIP from GitHub and extract it (or clone repository)

### 3. Open Hotel.sln in Microsoft Visual Studio

### 4. Configure JWT Secret

4.1. In the root project directory create a file:

```
appsettings.Development.json
```

4.2. In the file paste:
```
{
  "Jwt": {
    "Key": "secret_key"
  }
}
```

4.3. Replace *secret_key* with 32-characters long random string.

### 5.  In Visual Studio open *Tools -> NuGet Package Manager -> Package Manager Console* and run
```
 Update-Database
```

### 6. Run the application by clicking *http* button.

### 7. You can test API in Postman or Swagger. Application is running under *http://localhost:5269/api* address.