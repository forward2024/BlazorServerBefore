using Before.Data.Models;

namespace Before.Service.ServiceProduct
{
    public interface IProduct
    {
        Task GetAllAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> UpdateProductAsync(Product product);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(int Id);
    }
}
