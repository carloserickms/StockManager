Create new migration dotnet ef migrations add MaterialTable --startup-project .\src\api\StockManager.API --project .\src\infrastructure\StockManager.Infrastructure
dotnet add .\infrastructure\StockManager.Infrastructure\StockManager.Infrastructure.csproj reference .\application\StockManager.Application\StockManager.Application.csproj
