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
        public List<TypeItem> TypeItems { get; set; } = new List<TypeItem>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<ImageModel> ProductImages { get; set; } = new List<ImageModel>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Seller> Sellers { get; set; } = new List<Seller>();
        public List<Season> Seasons { get; set; } = new List<Season>();
        public List<Color> Colors { get; set; } = new List<Color>();
        public List<Size> Sizes { get; set; } = new List<Size>();
        


        public  Product GetItemById(int Id)
        {
            return Products.First(v => v.Id == Id);
        }
    }
}
