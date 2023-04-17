namespace Before.Data.Models
{
    public class SizeModel
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Size Size { get; set; }
        public int SizeId { get; set; }
        public Color Color { get; set; }
        public int ColorId{ get; set; }
        public int Count { get; set; }
    }
}
