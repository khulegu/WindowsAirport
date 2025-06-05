Migration үүсгэх:

```
dotnet ef migrations add InitialCreate --project AirportServer
```

Database-г шинэчлэх (үгүй бол програм ажиллахдаа үүсгэнэ):

```
dotnet ef database update --startup-project AirportServer
```

Server-г ажиллуулах:

```
dotnet run --project AirportServer
```

Dashboard-г ажиллуулах:

```
dotnet run --project Dashboard
```