using Before.Data;
using Before.Data.Models;
using Before.Service.ServiceBlazor;
using Microsoft.EntityFrameworkCore;

namespace Before.Service.ServiceAddition.ServiceImage
{
    public class ImageService : IImage
    {
        private readonly DbContextOptions<ApplicationDbContext> context;
        private BlazorService blazorService;

        public ImageService(DbContextOptions<ApplicationDbContext> context, BlazorService blazorService)
        {
            this.context = context;
            this.blazorService = blazorService;
        }

        public async Task GetAllAsync()
        {
            using (var contextUsing = new ApplicationDbContext(context))
            {
                blazorService.ProductImages = await contextUsing.ProductImages
                    .Include(v => v.Product)
                    .ToListAsync();
            }
        }

        public async Task SetImagesAsync(List<ImageModel> Files)
        {
            using (var contextUsing = new ApplicationDbContext(context))
            {
                foreach (var item in Files)
                {
                    contextUsing.ProductImages.Add(item);
                }
                await contextUsing.SaveChangesAsync();
            }
            blazorService.ProductImages.AddRange(Files);
        }

        public async Task DeleteImage(int Id)
        {
            var imageToRemove = blazorService.ProductImages.FirstOrDefault(v => v.Id == Id);
            if (imageToRemove != null)
            {
                blazorService.ProductImages.Remove(imageToRemove);
                blazorService.Changer();
                using (var contextUsing = new ApplicationDbContext(context))
                {
                    contextUsing.ProductImages.Remove(imageToRemove);
                    await contextUsing.SaveChangesAsync();
                }
            }
        }

        public async Task SwapProductImagesAsync(int firstId, int secondId)
        {
            int firstIndex = blazorService.ProductImages.FindIndex(image => image.Id == firstId);
            int secondIndex = blazorService.ProductImages.FindIndex(image => image.Id == secondId);

            if (firstIndex == -1 || secondIndex == -1)
            {
                throw new ArgumentException("One or both ProductImage Ids not found");
            }

            string tempImageUrlBlazor = blazorService.ProductImages[firstIndex].ImageURL;
            blazorService.ProductImages[firstIndex].ImageURL = blazorService.ProductImages[secondIndex].ImageURL;
            blazorService.ProductImages[secondIndex].ImageURL = tempImageUrlBlazor;
            blazorService.Changer();

            using (var contextUsing = new ApplicationDbContext(context))
            {
                var firstImage = await contextUsing.ProductImages.FindAsync(firstId);
                var secondImage = await contextUsing.ProductImages.FindAsync(secondId);

                if (firstImage == null || secondImage == null)
                {
                    throw new ArgumentException("One or both ProductImage Ids not found");
                }

                string tempImageUrl = firstImage.ImageURL;
                firstImage.ImageURL = secondImage.ImageURL;
                secondImage.ImageURL = tempImageUrl;
                await contextUsing.SaveChangesAsync();
            }
        }

    }
}
