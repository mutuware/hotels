# Hotels
Asp.Net repo for Hotels

## Deployed to Azure

https://hotels-api-bkb3fdbbddesbsbk.ukwest-01.azurewebsites.net/

## API documentation

https://localhost:7166/scalar/v1

## To add DB migration (from Solution root directory)

dotnet ef migrations add InitialMigration --project Hotels.Data --startup-project Hotels.Api

## To apply DB migration (this will also create a SQLite DB if none exists)

dotnet ef database update --project Hotels.Data --startup-project Hotels.Api

# Improvements

## Map Entities to DTO in API layer
## Handle errors globally with ProblemDetails
## Reset DB identity columns for better determinsim
## Allow external client to be able to save Hotel/Room/Booking data to allow setting up external test data
## Consider storing capacity value in DB for better performance due to filtering in the DB.
## Third party library such as ShortId can provide a more friendly unique ID rather than Guid