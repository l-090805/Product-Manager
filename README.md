Simple web app for managing products, built with Blazor WebAssembly and .NET

## What it does
 
- View a list of products in a table
- Add new products
- Edit existing products
- Delete products
- Automatically removes products with 0 stock every 24 hours (background job)

  
## Technologies used
 
- **Blazor WebAssembly** - frontend UI running in the browser
- **ASP.NET Core Web API** - backend with REST endpoints
- **Entity Framework Core** - database access and migrations
- **SQL Server 2022** - database running in Docker
- **Docker** - containerized SQL Server
- **Radzen Blazor** - UI component library (DataGrid, Dialogs, Notifications)
