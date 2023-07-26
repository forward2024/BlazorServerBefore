namespace Before.Data.Models
{
    public class Cart
    {
        public Product Product { get; set; }
        public int Count { get; set; }

        public Cart(Product product, int count)
        {
             this.Product = product;
             this.Count = count;    
        }
    }   
}
