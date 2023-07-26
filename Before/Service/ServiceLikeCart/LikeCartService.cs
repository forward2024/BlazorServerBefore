using Before.Data.Models;
using Before.Service.ServiceLikeCart;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

public class LikeCartService : ILikeCartService
{
    private readonly ITempDataDictionaryFactory tempDataDictionaryFactory;
    private readonly IHttpContextAccessor httpContextAccessor;

    public LikeCartService(ITempDataDictionaryFactory tempDataDictionaryFactory, IHttpContextAccessor httpContextAccessor)
    {
        this.tempDataDictionaryFactory = tempDataDictionaryFactory;
        this.httpContextAccessor = httpContextAccessor;
    }

    private void SetSession<T>(string key, T value)
    {
        var tempData = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
        tempData[key] = JsonConvert.SerializeObject(value);
    }

    private T GetFromSession<T>(string key)
    {
        var tempData = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
        if (tempData[key] is string sessionValue)
        {
            return JsonConvert.DeserializeObject<T>(sessionValue);
        }
        return default;
    }

    public void AddItemInLike(Product product)
    {
        var likes = GetLikes();
        likes.Add(new Cart(product, 1));
        SetSession("likes", likes);
    }

    public void RemoveItemFromLike(Product product)
    {
        var likes = GetLikes();
        likes.RemoveAll(x => x.Product.Id == product.Id);
        SetSession("likes", likes);
    }

    public void AddItemInCart(Product product)
    {
        var cart = GetCart();
        cart.Add(new Cart(product, 1));
        SetSession("cart", cart);
    }

    public void RemoveItemFromCart(Product product)
    {
        var cart = GetCart();
        cart.RemoveAll(x => x.Product.Id == product.Id);
        SetSession("cart", cart);
    }


    public List<Cart> GetCart()
    {
        return GetFromSession<List<Cart>>("cart") ?? new List<Cart>();
    }

    public List<Cart> GetLikes()
    {
        return GetFromSession<List<Cart>>("likes") ?? new List<Cart>();
    }
}
