using Before.Data.Models;

namespace Before.Service.ServiceProduct
{
    public interface IProduct
    {
        Task GetAllAsync();
        Task<int> AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int Id);
    }
}
