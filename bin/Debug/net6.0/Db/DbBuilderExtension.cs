namespace store.Db;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using store.Models;
using Newtonsoft.Json;

public static class DbBuilderExtension
{
    public static void AddBuilderConfig(this ModelBuilder modelBuilder)
    {
        // Seeding
        modelBuilder
            .Entity<IdentityRole<int>>()
            .HasData(
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<int>
                {
                    Id = 2,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
        // Relationship
        modelBuilder
            .Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<CartItem>().HasKey(ci => new { ci.CartId, ci.ProductId });
        modelBuilder
            .Entity<CartItem>()
            .HasOne<Product>(p => p.Product)
            .WithMany(c => c.CartItems)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Cart>().HasKey(c => c.Id);
        modelBuilder
            .Entity<Cart>()
            .HasOne<User>(c => c.User)
            .WithOne(u => u.Cart)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<User>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Cart>()
            .HasMany(c => c.CartItems)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId);

        modelBuilder.Entity<CartItem>().Property(ci => ci.Id).UseIdentityColumn();

        var catPath = Path.Combine(Environment.CurrentDirectory, "Db", "Json", "CategorySeeds.js");
        var categoryJson = File.ReadAllText(catPath);
        List<Category>? categories = null;
        if (categoryJson != null)
        {
            categories = JsonConvert.DeserializeObject<List<Category>>(categoryJson);
        }

        var prodPath = Path.Combine(Environment.CurrentDirectory, "Db", "Json", "ProductSeeds.js");
        var productJson = File.ReadAllText(prodPath);
        List<Product>? products = null;
        if (productJson != null)
        {
            products = JsonConvert.DeserializeObject<List<Product>>(productJson);
        }

        if (products != null && categories != null)
        {
            foreach (var product in products)
            {
                var category = categories.FirstOrDefault(c => c.Name == product.Category.Name);
                if (category != null)
                {
                    product.CategoryId = category.Id;
                }
                product.Category = null; // Remove the Category property to avoid conflicts
            }
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);
        }

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var createdAtProperty = entityType.FindProperty("CreationAt");
            var updatedAtProperty = entityType.FindProperty("UpdatedAt");

            if (createdAtProperty != null)
            {
                createdAtProperty.SetDefaultValueSql("CURRENT_TIMESTAMP");
            }

            if (updatedAtProperty != null)
            {
                updatedAtProperty.SetDefaultValueSql("CURRENT_TIMESTAMP");
            }
        }
    }
}
