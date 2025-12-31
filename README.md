# Golf-Bag-Manager-REST-API-Version
RESTful API built with ASP.NET Core Web API and Entity Framework Core, managing golf club inventory with validation, persistence, and Swagger documentation.

*Implemented database persistence using Entity Framework Core with SQLite and Table-per-Hierarchy inheritance for polymorphic club types
*Designed RESTful endpoints with proper HTTP verbs (GET, POST, DELETE) and status codes (200, 201, 400, 404) using JSON request/response models
*Applied dependency injection with interface-based architecture (IGolfBag, IClubFactory) following SOLID principles for testability
*Implemented Factory pattern for object creation and business logic validation (duplicate prevention, 14-club capacity limit)
