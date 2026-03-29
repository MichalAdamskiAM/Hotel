# Hotel

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Manual](#manual)

## General info

*Hotel* is an application that allows employees to manage the hotel operations and customers to book rooms. The project is currently under development.

## Technologies

The technologies used include Microsoft SQL Server, C#.NET, ASP.NET, and Entity Framework on the backend, as well as React, TypeScript, HTML, and CSS on the frontend.

Project follows RESTful API principles and uses JWT-based authentication.

## Setup

1. Install required software

1.1. Install Microsoft SQL Server Express and select *Basic* version.

1.2. Install Microsoft Visual Studio (*Community* version) with workloads:
* ASP.NET and web development
* Data storage and processing

1.3. You can also install SQl Server Management Studio (no additional workloads or components needed) if you want to see database.

2. Download ZIP from GitHub and extract it or clone repository

3. Open Hotel.sln in Microsoft Visual Studio

4.1.  In Visual Studio click open Tools -> NuGet Package Manager -> Package Manager Console

4.2. Run
> Install-Package Microsoft.EntityFrameworkCore -Version 8.0.10
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.10
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.10

4.3. Run
> Update-Database

4.4. Migrations are already included in the repository.

5. Run the application by clicking *http* button.

6. You can test API in web browser or Postman. Application is running under *http://localhost:5269/api/Reservations* address.



## Manual
