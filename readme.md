Create new migration: dotnet ef migrations add MaterialTable --startup-project .\src\StockManager --project .\src\infrastructure\StockManager.Infrastructure

Create reference: dotnet add .\infrastructure\StockManager.Infrastructure\StockManager.Infrastructure.csproj reference .\application\StockManager.Application\StockManager.Application.csproj

up migration: dotnet ef database update

