using System;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
    ): IdentityDbContext<IdentityUser>(options)
{   

    #region --Db Sets--
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<WarehouseStock> WarehouseStocks => Set<WarehouseStock>();
    public DbSet<StockTransaction> StockTransactions => Set<StockTransaction>();

    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
            .HasIndex(p => p.Sku)
            .IsUnique();

        builder.Entity<WarehouseStock>()
        .HasKey(ws => new {ws.ProductId, ws.WarehouseId});

        builder.Entity<Product>()
        .Property(p => p.Price)
        .HasPrecision(18, 2);

        builder.Entity<WarehouseStock>()
        .HasOne(ws => ws.Product)
        .WithMany(p => p.WarehouseStocks)
        .HasForeignKey(ws => ws.ProductId);

        builder.Entity<WarehouseStock>()
        .HasOne(ws => ws.Warehouse)
        .WithMany()
        .HasForeignKey(ws => ws.WarehouseId);

        //seeder

        builder.Entity<Category>()
        .HasData(
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Office Supplies" }
        );
    }
}
