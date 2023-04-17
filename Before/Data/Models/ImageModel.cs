namespace Before.Data.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
