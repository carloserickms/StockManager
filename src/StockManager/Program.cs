using infrastructure.StockManager.Infrastructure.Configurations;
using infrastructure.StockManager.Infrastructure.Persistence;
using StockManager.Middlewares;
using StockManager.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddOpenApi();
builder.Services.AddControllers();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (!string.IsNullOrEmpty(connectionString))
{
    AppConfig.StartDependences(builder.Services, connectionString);
}

// 1️⃣ Adiciona CORS nos serviços
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.MapControllers();

// Seed inicial
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ColorTableSeed.Seed(context);
    PaymentMethodSeed.Seed(context);
    StatusSeed.Seed(context);
}

app.Run();
