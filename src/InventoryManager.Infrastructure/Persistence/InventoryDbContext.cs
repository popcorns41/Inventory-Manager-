using System.Dynamic;
using InventoryManager.Domain.Categories;
using InventoryManager.Domain.Products;
using InventoryManager.Domain.Suppliers;
using InventoryManager.Domain.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Infrastructure.Persistence;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Supplier> Suppliers => Set<Supplier>();

    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    public DbSet<WarehouseStock> WarehouseStocks => Set<WarehouseStock>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.ToTable("warehouses");

            entity.HasKey(warehouse => warehouse.Id);

            entity.Property(warehouse => warehouse.Id).HasColumnName("id");

            entity.Property(warehouse => warehouse.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();
            
            entity.HasIndex(warehouse => warehouse.Name).IsUnique();

            entity.Property(warehouse => warehouse.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);
        }
        );

        modelBuilder.Entity<WarehouseStock>(entity =>
        {
            entity.ToTable("warehouse_stock");

            entity.HasKey(stock => new
            {
                stock.ProductId,
                stock.WarehouseId
            });

            entity.Property(stock => stock.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            entity.Property(stock => stock.WarehouseId)
                .HasColumnName("warehouse_id")
                .IsRequired();

            entity.Property(stock => stock.QuantityOnHand)
                .HasColumnName("quantity_on_hand")
                .IsRequired();

            entity.HasOne<Product>()
                .WithMany()
                .HasForeignKey(stock => stock.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(stock => stock.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Category>(entity =>
        {
           entity.ToTable("categories");

           entity.HasKey(category => category.Id);

           entity.Property(category => category.Id).HasColumnName("id");

            entity.Property(category => category.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();
            
            entity.HasIndex(category => category.Name).IsUnique();

            entity.Property(category => category.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);
            

        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("suppliers");

            entity.HasKey(supplier => supplier.Id);

            entity.Property(supplier => supplier.Id).HasColumnName("id");

            entity.Property(supplier => supplier.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();
            
            entity.HasIndex(supplier => supplier.Name).IsUnique();

            entity.Property(supplier => supplier.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);
        });


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
            
            entity.Property(product => product.CategoryId)
                .HasColumnName("category_id");
            
            entity.HasOne<Category>()
                .WithMany()
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(product => product.SupplierId)
                .HasColumnName("supplier_id");

            entity.HasOne<Supplier>()
                .WithMany()
                .HasForeignKey(product => product.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}