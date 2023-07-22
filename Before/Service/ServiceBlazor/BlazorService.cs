using Before.Data.Models;

namespace Before.Service.ServiceBlazor
{
    public class BlazorService
    {
        public event Action ProductAdded;
        public void Changer()
        {
            ProductAdded?.Invoke();
        }
        public HashSet<TypeItem> TypeItems { get; set; } = new HashSet<TypeItem>();
        public List<Product> Products { get; set; } = new List<Product>();
        public HashSet<Category> Categories { get; set; } = new HashSet<Category>();
        public HashSet<Seller> Sellers { get; set; } = new HashSet<Seller>();
        public HashSet<Season> Seasons { get; set; } = new HashSet<Season>();
        public HashSet<Color> Colors { get; set; } = new HashSet<Color>();
        public HashSet<Size> Sizes { get; set; } = new HashSet<Size>();
        


        public  Product GetItemById(int Id)
        {
            return Products.First(v => v.Id == Id);
        }
    }
}
