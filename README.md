# Hotels
Asp.Net repo for Hotels

## Deployed to Azure

https://hotels-api-bkb3fdbbddesbsbk.ukwest-01.azurewebsites.net/

## Tech

- Net 9
- Scalar for OpenAPI documentation
- SQLite for persistence

## Improvements

- Map Entities to DTO in API layer
- Handle errors globally with ProblemDetails
- Reset DB identity columns for better determinsim
- Allow external client to be able to save Hotel/Room/Booking data to allow setting up external test data
- Consider storing capacity value in DB for better performance due to filtering in the DB.
- Third party library such as ShortId can provide a more friendly unique ID rather than Guid


## DB commands

### To add DB migration (from Solution root directory)

```
dotnet ef migrations add InitialMigration --project Hotels.Data --startup-project Hotels.Api
```

### To apply DB migration (this will also create a SQLite DB if none exists)

```
dotnet ef database update --project Hotels.Data --startup-project Hotels.Api
```

## Example GET Queries

### Hotels

All hotels  
https://hotels-api-bkb3fdbbddesbsbk.ukwest-01.azurewebsites.net/api/hotels/

### Bookings

All bookings  
https://hotels-api-bkb3fdbbddesbsbk.ukwest-01.azurewebsites.net/api/bookings

Booking by reference  
https://hotels-api-bkb3fdbbddesbsbk.ukwest-01.azurewebsites.net/api/bookings?reference=45241004-a698-46d8-897b-fd3d67265863


### Availability

Raffles room are unavailable in December  
https://hotels-api-bkb3fdbbddesbsbk.ukwest-01.azurewebsites.net/api/availability?from=2024-12-01T00:00:00.000Z&to=2024-12-02T00:00:00.000Z&people=2

All rooms available in November  
https://hotels-api-bkb3fdbbddesbsbk.ukwest-01.azurewebsites.net/api/availability?from=2024-11-01T00:00:00.000Z&to=2024-11-02T00:00:00.000Z&people=2

No 3 people rooms available  
https://hotels-api-bkb3fdbbddesbsbk.ukwest-01.azurewebsites.net/api/availability?from=2024-11-01T00:00:00.000Z&to=2024-11-02T00:00:00.000Z&people=3
