using InventoryManager.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Infrastructure.Persistence;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.HasKey(product => product.Id);

            entity.Property(product => product.Id)
                .HasColumnName("id");

            entity.Property(product => product.Sku)
                .HasColumnName("sku")
                .HasMaxLength(50)
                .IsRequired();

            entity.HasIndex(product => product.Sku)
                .IsUnique();

            entity.Property(product => product.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(product => product.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);

            entity.Property(product => product.Price)
                .HasColumnName("price")
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            entity.Property(product => product.QuantityInStock)
                .HasColumnName("quantity_in_stock")
                .IsRequired();
        });
    }
}