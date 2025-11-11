using application.StockManager.Application.Interfaces.Repositories;
using application.StockManager.Application.Interfaces.Services;
using application.StockManager.Application.Service;
using infrastructure.StockManager.Infrastructure.Persistence;
using infrastructure.StockManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.StockManager.Infrastructure.Configurations
{
    public static class AppConfig
    {
        public static void StartDependences(IServiceCollection services, string connectionString)
        {
            try
            {
                ConfigureDataBase(services, connectionString);
                ConfigureDependencyInjection(services);
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao iniciar as dependencias: {ex.Message}", ex);
            }
        }

        public static void ConfigureDataBase(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("StockManager.Infrastructure")));

        }

        public static void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IGenderRepository, GenderRepository>();
        }
    }
}