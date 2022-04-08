# GarageVolver

A code Assessment test. Developed with Asp.Net Core 6.

## Run instructions:
On the root of the solution, open powershell terminal and type:
```
dotnet run --project .\GarageVolver.API\
```
When the server is up, acess swagger endpoin at [localhost:7108/swagger/](https://localhost:7108/swagger/index.html)

## Main Requirement
It is needed to make a truck registry with the capability to:
 * See all registered trucks;
 * Truck minimal properties are:
   * Model (acepting only FH and FM);
   * Year of manufature (must be current year);
   * Model year (can be current or next year).
 * Update truk registry;
 * Delete a truck;
 * insert new truck.

## Technical
Made using ASP.NET Core, SQLite local file, EntityFramework ORM, automatic Migrations and TDD.
