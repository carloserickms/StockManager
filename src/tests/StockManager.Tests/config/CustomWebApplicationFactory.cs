using infrastructure.StockManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 1. Remove o DbContext original (MySQL)
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // 2. Adiciona o DbContext em memória
            services.AddDbContext<AppDbContext>(options =>
            {
                // Use Guid para criar um banco único por teste
                options.UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}");
            });

            // 3. Constrói o service provider e cria o banco
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        });
    }
}
