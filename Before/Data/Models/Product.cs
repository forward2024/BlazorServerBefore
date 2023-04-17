
namespace Before.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Action { get; set; }
        public List<ImageModel> Images { get; set; } = new List<ImageModel>();
        public List<SeasonModel> Seasons { get; set; } = new List<SeasonModel>();
        public List<SizeModel> Sizes { get; set; } = new List<SizeModel>();
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Seller Seller { get; set; }
        public int SellerId { get; set; }
        public TypeItem TypeItem { get; set; }
        public int TypeItemId { get; set; }
    }
}
