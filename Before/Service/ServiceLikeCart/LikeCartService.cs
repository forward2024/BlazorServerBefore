using Before.Data.Models;

namespace Before.Service.ServiceLikeCart
{
    public class LikeCartService : ILikeCartService
    {
        public List<Product> LikeProducts { get; private set; } = new List<Product>();

        public List<Product> CartProducts { get; private set; } = new List<Product>();

        public Task AddItemInCart(Product product)
        {
            throw new NotImplementedException();
        }

        public Task AddItemInLike(Product product)
        {
            throw new NotImplementedException();
        }

        public Task Decrement()
        {
            throw new NotImplementedException();
        }

        public Task Increment()
        {
            throw new NotImplementedException();
        }

        public Task RemoveItemIntoCart(Product product)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItemIntoLike(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
