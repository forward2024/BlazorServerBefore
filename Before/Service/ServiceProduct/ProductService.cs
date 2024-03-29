﻿using Before.Data;
using Before.Data.Models;
using Microsoft.CodeAnalysis;
using MongoDB.Driver;

namespace Before.Service.ProductService
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext context;
        private readonly MongoDbContext mongoContext;
        private BlazorService blazorService;

        public ProductService(ApplicationDbContext context, MongoDbContext mongoContext, BlazorService blazorService)
        {
            this.context = context;
            this.mongoContext = mongoContext;
            this.blazorService = blazorService;
        }

        public async Task GetAllAsync()
        {
            var sqlProducts = await context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.TypeItem)
                .ToListAsync();

            var mongoImages = await mongoContext.MongoDBProducts.Find(_ => true).ToListAsync();
            blazorService.Products = CombineData(sqlProducts, mongoImages);
        }

        private List<Product> CombineData(List<Product> sqlProducts, List<MongoDBProduct> mongoProducts)
        {
            foreach (var product in sqlProducts)
            {
                var relatedMongoProduct = mongoProducts.FirstOrDefault(p => p.Id == product.Id);

                var convertedImages = ConvertImagesToBase64(relatedMongoProduct.Images);
                product.Images.AddRange(convertedImages);

                product.Seasons = new HashSet<string>(relatedMongoProduct.Seasons);
                product.Sizes = new HashSet<string>(relatedMongoProduct.Sizes);
                product.Colors = new HashSet<string>(relatedMongoProduct.Colors);
            }

            return sqlProducts;
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await context.Products
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.TypeItem)
                .FirstAsync(p => p.Id == id);

            var mongoProduct = await mongoContext.MongoDBProducts.Find(p => p.Id == id).FirstAsync();
            var convertedImages = ConvertImagesToBase64(mongoProduct.Images);

            product.Images = convertedImages;
            product.Seasons = new HashSet<string>(mongoProduct.Seasons);
            product.Sizes = new HashSet<string>(mongoProduct.Sizes);
            product.Colors = new HashSet<string>(mongoProduct.Colors);

            return product;
        }


        public async Task AddProductAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();

            var mongoDBProduct = new MongoDBProduct
            {
                Id = product.Id,
                Images = ConvertBase64ToBytes(product.Images),
                Seasons = product.Seasons,
                Sizes = product.Sizes,
                Colors = product.Colors
            };

            await mongoContext.MongoDBProducts.InsertOneAsync(mongoDBProduct);

            (product.Seller, product.Category, product.TypeItem) = (
                blazorService.Sellers.First(v => v.Id == product.SellerId),
                blazorService.Categories.First(c => c.Id == product.CategoryId),
                blazorService.TypeItems.First(t => t.Id == product.TypeItemId));

            blazorService.Products.Add(product);
            blazorService.Changer();
        }


        public async Task<bool> UpdateProductAsync(Product product)
        {
            bool result = false;
            if (context.Entry(product).State == EntityState.Modified)
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();
                context.Entry(product).State = EntityState.Detached;

                (product.Seller, product.Category, product.TypeItem) = (
                    blazorService.Sellers.First(v => v.Id == product.SellerId),
                    blazorService.Categories.First(c => c.Id == product.CategoryId),
                    blazorService.TypeItems.First(t => t.Id == product.TypeItemId));

                blazorService.Products.Remove(blazorService.GetItemById(product.Id));
                blazorService.Products.Add(product);
                blazorService.Changer();
                result = true;
            }


            var originalMongoProduct = await mongoContext.MongoDBProducts.Find(p => p.Id == product.Id).FirstAsync();
            Product originalProduct = new Product
            {
                Images = ConvertImagesToBase64(originalMongoProduct.Images),
                Seasons = new HashSet<string>(originalMongoProduct.Seasons),
                Sizes = new HashSet<string>(originalMongoProduct.Sizes),
                Colors = new HashSet<string>(originalMongoProduct.Colors)
            };
            if (product.HasChanges(originalProduct))
            {
                var mongoProduct = new MongoDBProduct
                {
                    Id = product.Id,
                    Images = ConvertBase64ToBytes(product.Images),
                    Seasons = product.Seasons,
                    Sizes = product.Sizes,
                    Colors = product.Colors
                };
                var filter = Builders<MongoDBProduct>.Filter.Eq("_id", product.Id);
                await mongoContext.MongoDBProducts.ReplaceOneAsync(filter, mongoProduct);

                var existingProduct = blazorService.Products.FirstOrDefault(p => p.Id == product.Id);
                blazorService.Products.Remove(existingProduct);
                blazorService.Products.Add(product);
                blazorService.Changer();

                result = true;
            }
            return result;
        }
        private List<byte[]> ConvertBase64ToBytes(List<string> base64Images)
        {
            List<byte[]> result = new();
            foreach (var base64Image in base64Images)
            {
                var imageBytes = Convert.FromBase64String(base64Image.Split(",")[1]);
                result.Add(imageBytes);
            }
            return result;
        }
        private List<string> ConvertImagesToBase64(List<byte[]> imageBytes)
        {
            List<string> result = new();
            foreach (var item in imageBytes)
            {
                var base64ImageRepresentation = Convert.ToBase64String(item);
                result.Add($"data:image/jpeg;base64,{base64ImageRepresentation}");
            }
            return result;
        }


        public async Task SwapImagesInMongoDB(int productId, string image)
        {
            var filter = Builders<MongoDBProduct>.Filter.Eq(s => s.Id, productId);
            var mongoProduct = await mongoContext.MongoDBProducts.Find(filter).FirstOrDefaultAsync();

            string base64Image = image.Substring(image.IndexOf(',') + 1);
            if (IsBase64String(base64Image))
            {
                var imageBytes = Convert.FromBase64String(base64Image);

                if (mongoProduct.Images.Remove(imageBytes))
                {
                    mongoProduct.Images.Insert(0, imageBytes);
                }

                await mongoContext.MongoDBProducts.ReplaceOneAsync(filter, mongoProduct);
            }
        }
        private bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out _);
        }


        public async Task DeleteProductAsync(int id)
        {
            var productInBlazorService = blazorService.Products.Find(p => p.Id == id);
            blazorService.Products.Remove(productInBlazorService);

            var productInSqlServer = await context.Products.FindAsync(id);
            context.Products.Remove(productInSqlServer);
            await context.SaveChangesAsync();

            var filter = Builders<MongoDBProduct>.Filter.Eq("_id", id);
            await mongoContext.MongoDBProducts.DeleteOneAsync(filter);
        }
    }
}