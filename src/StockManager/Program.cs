using infrastructure.StockManager.Infrastructure.Configurations;
using infrastructure.StockManager.Infrastructure.Persistence;
using StockManager.Middlewares;
using StockManager.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (!string.IsNullOrEmpty(connectionString))
{
    AppConfig.StartDependences(builder.Services, connectionString);
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();


app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ColorTableSeed.Seed(context);
    PaymentMethodSeed.Seed(context);
    StatusSeed.Seed(context);
}

app.Run();
