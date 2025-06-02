# Library App
A simple application to manage book borrowing for a library. Built using .Net Core 9.

## Getting started
add your connection string to the ```appsettings.json```.

```json
{
    ...
    "ConnectionStrings": {
      "AppDb": "Host=<DB_HOST>;Database=<DB_NAME>;Username=<USERNAME>;Password=<PASSWORD>"
    }
    ...
}
```

to run the app in development mode, run this command  
```bash
dotnet run --project LibraryApp.Api
```  
or  
```bash
dotnet watch --project LibraryApp.Api
```