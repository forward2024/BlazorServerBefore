using Before.Data.Models;

namespace Before.Service.ServiceLikeCart
{
    public interface ILikeCartService
    {
        List<Product> LikeProducts { get; }
        Task AddItemInLike(Product product);
        Task RemoveItemIntoLike(Product product);
        List<Product> CartProducts { get; }
        Task AddItemInCart(Product product);
        Task RemoveItemIntoCart(Product product);
        Task Increment();
        Task Decrement();
    }
}
