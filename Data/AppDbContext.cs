using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using System;

namespace MyApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed User
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                // Hashed password for "Pass@123"
                PasswordHash = "$2b$11$N3VV3pY3eVD6ml.Eo.PRa.58wjNxyhgcB2f2e8YVWN5Ed18znzigu",
                Role = "Admin"
            });

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop Pro", Price = 1200.00m, CreatedAt = DateTime.UtcNow },
                new Product { Id = 2, Name = "Wireless Mouse", Price = 25.50m, CreatedAt = DateTime.UtcNow },
                new Product { Id = 3, Name = "Mechanical Keyboard", Price = 75.00m, CreatedAt = DateTime.UtcNow },
                new Product { Id = 4, Name = "4K Monitor", Price = 450.00m, CreatedAt = DateTime.UtcNow },
                new Product { Id = 5, Name = "Webcam HD", Price = 60.00m, CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
