using domain.StockManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.StockManager.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<ServiceOrder> ServiceOrder { get; set; }
        public DbSet<Gender> Gender { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }
}