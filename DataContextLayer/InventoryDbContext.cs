using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DataContextLayer
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext() { }
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        private string _connectionString = @"Server=SAYEEDS-CODING-\SQLEXPRESS;Database=InventoryDb;Trusted_Connection=True;";


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(a => a.Category).HasForeignKey(a => a.CategoryId);
            
            modelBuilder.Entity<Product>()
                .Property(a => a.Name).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Product>()
                .Property(a => a.Price).IsRequired();
            modelBuilder.Entity<Product>()
                .HasMany(a => a.Stocks)
                .WithOne(c => c.Product).HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<Stock>()
                .Property(a => a.Quantity).IsRequired();
            modelBuilder.Entity<Stock>()
                .Property(a => a.Price).IsRequired();
            modelBuilder.Entity<Stock>()
                .Property(a => a.ProductId).IsRequired();

            modelBuilder.Entity<Customer>()
                .Property(a => a.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Customer>()
                .Property(a => a.Address).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Customer>()
                .Property(a => a.Contact).IsRequired().HasMaxLength(20);
            //how to set email is not required?

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Sales)
                .WithOne(a => a.Customer).HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<Product>()
                .HasMany(b => b.Sales)
                .WithOne(a => a.Product).HasForeignKey(a => a.ProductId);
        }
    }
}
