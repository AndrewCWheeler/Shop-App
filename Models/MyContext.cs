using Microsoft.EntityFrameworkCore;

namespace ShopApp.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

            public DbSet<User> Users { get; set; }

            public DbSet<Category> Categories { get; set; }

            public DbSet<Ingredient> Ingredients { get; set; }

            public DbSet<Item> Items { get; set; }

            public DbSet<ItemCategory> ItemCategories { get; set; }

            public DbSet<ProductIngredient> ProductIngredients { get; set; }

            public DbSet<Price> Prices { get; set; }

            public DbSet<Product> Products { get; set; }

            public DbSet<Store> Stores { get; set; }


    }
}