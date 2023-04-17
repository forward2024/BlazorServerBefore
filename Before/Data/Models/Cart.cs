namespace Before.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }   
}
