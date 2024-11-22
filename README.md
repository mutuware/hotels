# Hotels
Asp.Net repo for hotels

## API documentation

https://localhost:7166/scalar/v1

## To add DB migration (from Solution root directory)

dotnet ef migrations add InitialMigration --project Hotels.Data --startup-project Hotels.Api

## To apply DB migration

dotnet ef database update --project Hotels.Data --startup-project Hotels.Api


# Improvements

## Map Entities to DTO in API layer
## Handle errors
