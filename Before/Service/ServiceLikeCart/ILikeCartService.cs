using Before.Data.Models;

namespace Before.Service.ServiceLikeCart
{
    public interface ILikeCartService
    {
        void AddItemInLike(Product product);
        void RemoveItemFromLike(Product product);
        void AddItemInCart(Product product);
        void RemoveItemFromCart(Product product);
        List<Cart> GetCart();
        List<Cart> GetLikes();
    }
}
