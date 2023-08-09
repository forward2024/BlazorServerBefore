using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Before.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Action { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Seller Seller { get; set; }
        public int SellerId { get; set; }
        public TypeItem TypeItem { get; set; }
        public int TypeItemId { get; set; }
        [NotMapped]
        public List<string> Images { get; set; } = new List<string>();
        [NotMapped]
        public HashSet<string> Seasons { get; set; } = new HashSet<string>();
        [NotMapped]
        public HashSet<string> Sizes { get; set; } = new HashSet<string>();
        [NotMapped]
        public HashSet<string> Colors { get; set; } = new HashSet<string>();

        public Product() { }

        public Product(Product other)
        {
            this.Id = other.Id;
            this.Name = other.Name;
            this.Description = other.Description;
            this.Price = other.Price;
            this.Action = other.Action;
            this.CategoryId = other.CategoryId;
            this.SellerId = other.SellerId;
            this.TypeItemId = other.TypeItemId;

            this.Category = other.Category != null ? new Category(other.Category) : null;
            this.Seller = other.Seller != null ? new Seller(other.Seller) : null;
            this.TypeItem = other.TypeItem != null ? new TypeItem(other.TypeItem) : null;

            this.Seasons = new HashSet<string>(other.Seasons);
            this.Sizes = new HashSet<string>(other.Sizes);
            this.Colors = new HashSet<string>(other.Colors);
            this.Images = other.Images?.ToList();
        }
        public bool HasChanges(Product other)
        {
            if (!this.Sizes.SetEquals(other.Sizes) || !this.Seasons.SetEquals(other.Seasons) ||
                !this.Images.SequenceEqual(other.Images) || !this.Images.SequenceEqual(other.Colors))
                return true;
            return false;
        }
    }
}
