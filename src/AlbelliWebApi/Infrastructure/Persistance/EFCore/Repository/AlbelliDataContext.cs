using AlbelliWebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlbelliWebApi.Infrastructure.Persistance.EFCore.Repository
{
    public class AlbelliDataContext : DbContext
    {
        public AlbelliDataContext(DbContextOptions<AlbelliDataContext> options)
           : base(options)
        {
            LoadProducts();
        }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public void LoadProducts()
        {
            if (!ProductTypes.Any())
            {
                ProductTypes.Add(new ProductType()
                {
                    Id = 1,
                    Name = "PhotoBook",
                    PackageWidth = 19,
                    StackUpLimit = 1
                });
                ProductTypes.Add(new ProductType()
                {
                    Id = 2,
                    Name = "Calendar",
                    PackageWidth = 10,
                    StackUpLimit = 1
                });
                ProductTypes.Add(new ProductType()
                {
                    Id = 3,
                    Name = "Canvas",
                    PackageWidth = 16,
                    StackUpLimit = 1
                });
                ProductTypes.Add(new ProductType()
                {
                    Id = 4,
                    Name = "Cards",
                    PackageWidth = 4.7,
                    StackUpLimit = 1
                });
                ProductTypes.Add(new ProductType()
                {
                    Id = 5,
                    Name = "Mug",
                    PackageWidth = 94,
                    StackUpLimit = 4
                });
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
        }

    }

    
}
