using Before.Data;
using Before.Data.Models;
using Before.Service.ServiceBlazor;
using Before.Service.ServiceProduct;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace Before.Service.ProductService
{
    public class ProductService : IProduct
    {
        private readonly DbContextOptions<ApplicationDbContext> context;
        private BlazorService blazorService;

        public ProductService(DbContextOptions<ApplicationDbContext> context, BlazorService blazorService)
        {
            this.context = context;
            this.blazorService = blazorService;
        }


        public async Task GetAllAsync()
        {
            using (var contextUsing = new ApplicationDbContext(context))
            {
                blazorService.Products = await contextUsing.Products
                    .Include(p => p.Category)
                    .Include(p => p.Seller)
                    .Include(p => p.TypeItem)
                    .Include(p => p.Images)
                    .Include(p => p.Seasons).ThenInclude(ps => ps.Season)
                    .Include(p => p.Sizes).ThenInclude(ps => ps.Size)
                    .Include(p => p.Sizes).ThenInclude(ps => ps.Color)
                    .ToListAsync();
            }
        }

        public async Task<int> AddProductAsync(Product product)
        {
            using (var contextUsing = new ApplicationDbContext(context))
            {
                contextUsing.Products.Add(product);
                await contextUsing.SaveChangesAsync();

                product = await contextUsing.Products
                    .Include(p => p.Category)
                    .Include(p => p.Seller)
                    .Include(p => p.TypeItem)
                    .Include(p => p.Seasons).ThenInclude(ps => ps.Season)
                    .Include(p => p.Sizes).ThenInclude(ps => ps.Size)
                    .Include(p => p.Sizes).ThenInclude(ps => ps.Color)
                    .FirstOrDefaultAsync(p => p.Id == product.Id);
            }

            blazorService.Products.Add(product);
            blazorService.Changer();
            return product.Id;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var item = blazorService.GetItemById(product.Id);
            if (item.GetHashCode() != product.GetHashCode())
            {
                using (var contextUsing = new ApplicationDbContext(context))
                {
                    // Get the existing product with related data from the database
                    var existingProduct = await contextUsing.Products
                        .Include(p => p.Category)
                        .Include(p => p.Seller)
                        .Include(p => p.TypeItem)
                        .Include(p => p.Seasons).ThenInclude(ps => ps.Season)
                        .Include(p => p.Sizes).ThenInclude(ps => ps.Size)
                        .Include(p => p.Sizes).ThenInclude(ps => ps.Color)
                        .FirstOrDefaultAsync(p => p.Id == product.Id);

                    if (existingProduct != null)
                    {
                        // Detach the related entities
                        foreach (var season in existingProduct.Seasons)
                        {
                            contextUsing.Entry(season.Season).State = EntityState.Detached;
                        }

                        foreach (var size in existingProduct.Sizes)
                        {
                            contextUsing.Entry(size.Size).State = EntityState.Detached;
                            contextUsing.Entry(size.Color).State = EntityState.Detached;
                        }

                        // Update the existing product with the new data
                        existingProduct.Name = product.Name;
                        existingProduct.Description = product.Description;
                        existingProduct.Price = product.Price;
                        existingProduct.Action = product.Action;
                        existingProduct.CategoryId = product.CategoryId;
                        existingProduct.SellerId = product.SellerId;
                        existingProduct.TypeItemId = product.TypeItemId;
                        existingProduct.Seasons = product.Seasons;
                        existingProduct.Sizes = product.Sizes;

                        // Update the product in the database
                        contextUsing.Products.Update(existingProduct);
                        await contextUsing.SaveChangesAsync();
                    }
                }
                UpdateLocalProduct(product);
            }
            blazorService.Changer();
        }



        private void UpdateLocalProduct(Product product)
        {
            var item = blazorService.Products.FirstOrDefault(p => p.Id == product.Id);

            if (item != null)
            {
                int index = blazorService.Products.IndexOf(item);
                blazorService.Products[index] = product;
            }
        }

        public async Task DeleteProductAsync(int Id)
        {
            var productToRemove = blazorService.Products.FirstOrDefault(v => v.Id == Id);
            if (productToRemove != null)
            {
                blazorService.Products.Remove(productToRemove);
                blazorService.Changer();

                using (var contextUsing = new ApplicationDbContext(context))
                {
                    contextUsing.Products.Remove(productToRemove);
                    await contextUsing.SaveChangesAsync();
                }
            }
        }
    }
}