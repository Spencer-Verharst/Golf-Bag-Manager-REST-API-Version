# Golf-Bag-Manager-REST-API-Version
RESTful API built with ASP.NET Core Web API and Entity Framework Core, managing golf club inventory with validation, persistence, and Swagger documentation.

* Implemented database persistence using Entity Framework Core with SQLite and Table-per-Hierarchy inheritance for polymorphic club types
* Designed RESTful endpoints with proper HTTP verbs (GET, POST, DELETE) and status codes (200, 201, 400, 404) using JSON request/response models
* Applied dependency injection with interface-based architecture (IGolfBag, IClubFactory) following SOLID principles for testability
* Implemented Factory pattern for object creation and business logic validation (duplicate prevention, 14-club capacity limit)

## API Endpoints

- `GET /api/golfbag` - Get all clubs
- `POST /api/golfbag` - Add a club
- `DELETE /api/golfbag/{clubType}` - Remove a club
- `GET /api/golfbag/count` - Get club count
- `GET /api/golfbag/{clubType}/exists` - Check if club exists

## How to Run

1. Clone the repository
2. Open in Visual Studio 2022
3. Restore NuGet packages
4. Press F5 to run
5. Navigate to https://localhost:5001/swagger

## Sample Request
```json
POST /api/golfbag
{
  "type": "Driver",
  "brand": "TaylorMade",
  "distance": 275
}
