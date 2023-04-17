using Before.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Before.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TypeItem> TypeItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImageModel> ProductImages { get; set; }
        public DbSet<SizeModel> SizeProducts { get; set; }
        public DbSet<SeasonModel> ProductSeasons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Cart> Carts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => x.UserId);

            modelBuilder.Entity<SeasonModel>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Name = "#ff0000" }, // Red
                new Color { Id = 2, Name = "#ffffff" }, // White
                new Color { Id = 3, Name = "#000000" }, // Black
                new Color { Id = 4, Name = "#0000ff" }, // Blue
                new Color { Id = 5, Name = "#008000" }, // Green
                new Color { Id = 6, Name = "#ffff00" }, // Yellow
                new Color { Id = 7, Name = "#800080" }, // Purple
                new Color { Id = 8, Name = "#ffa500" }, // Orange
                new Color { Id = 9, Name = "#ff69b4" }, // Hot Pink
                new Color { Id = 10, Name = "#1e90ff" }, // Dodger Blue
                new Color { Id = 11, Name = "#f0e68c" }, // Khaki
                new Color { Id = 12, Name = "#ff4500" }, // Orange Red
                new Color { Id = 13, Name = "#6a5acd" }, // Slate Blue
                new Color { Id = 14, Name = "#d2691e" }, // Chocolate
                new Color { Id = 15, Name = "#2e8b57" }, // Sea Green
                new Color { Id = 16, Name = "#f5deb3" }, // Wheat
                new Color { Id = 17, Name = "#7b68ee" }, // Medium Slate Blue
                new Color { Id = 18, Name = "#dda0dd" }, // Plum
                new Color { Id = 19, Name = "#5f9ea0" }, // Cadet Blue
                new Color { Id = 20, Name = "#ffe4b5" } // Moccasin
            );

            modelBuilder.Entity<Seller>().HasData(
                new Seller { Id = 1, Name = "FashionHub" },
                new Seller { Id = 2, Name = "StyleStore" },
                new Seller { Id = 3, Name = "TrendyMarket" },
                new Seller { Id = 4, Name = "ModernWear" },
                new Seller { Id = 5, Name = "UrbanOutfits" }
            );

            modelBuilder.Entity<Size>().HasData(
                new Size { Id = 1, Name = "XS" },
                new Size { Id = 2, Name = "S" },
                new Size { Id = 3, Name = "M" },
                new Size { Id = 4, Name = "L" },
                new Size { Id = 5, Name = "XL" }
            );

            modelBuilder.Entity<Season>().HasData(
                new Season { Id = 1, Name = "Spring" },
                new Season { Id = 2, Name = "Summer" },
                new Season { Id = 3, Name = "Autumn" },
                new Season { Id = 4, Name = "Winter" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "T-Shirts" },
                new Category { Id = 2, Name = "Jeans" },
                new Category { Id = 3, Name = "Shirts" },
                new Category { Id = 4, Name = "Jackets" },
                new Category { Id = 5, Name = "Sweaters" },
                new Category { Id = 6, Name = "Dresses" },
                new Category { Id = 7, Name = "Skirts" },
                new Category { Id = 8, Name = "Shoes" },
                new Category { Id = 9, Name = "Bags" },
                new Category { Id = 10, Name = "Accessories" }
            );

            modelBuilder.Entity<TypeItem>().HasData(
                new TypeItem { Id = 1, Name = "Accessories" },
                new TypeItem { Id = 2, Name = "Clothing" },
                new TypeItem { Id = 3, Name = "Perfumes" }
            );
        }
    }
}