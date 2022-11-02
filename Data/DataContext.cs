using Microsoft.EntityFrameworkCore;
using Model;

namespace Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.OrderId, op.ProductId });

        modelBuilder.Entity<User>().HasData(
            new { Id = Guid.NewGuid(), Email = "test@email.com", Password = "1234" }
        );

        modelBuilder.Entity<Product>().HasData(
            new { Id = Guid.NewGuid(), Name = "Product 1", ImageId = new Guid("82078175-0e8c-453a-a71c-9e6c80b25c70") },
            new { Id = Guid.NewGuid(), Name = "Product 2", ImageId = new Guid("383ee594-db7d-41c5-8766-e63fe76cb9d9") },
            new { Id = Guid.NewGuid(), Name = "Product 3", ImageId = new Guid("43ccb5a5-56a6-4182-b318-243d200d9a30") },
            new { Id = Guid.NewGuid(), Name = "Product 4", ImageId = new Guid("af5f4ed2-7db4-42f2-b5af-33662550e315") }
        );
    }
}
