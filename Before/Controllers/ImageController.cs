using Before.Data.Models;
using Before.Service.ServiceAddition.ServiceImage;
using Microsoft.AspNetCore.Mvc;

namespace Before.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImage imageService;
        private readonly IProduct productService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ImageController(IWebHostEnvironment hostingEnvironment, IImage imageService, IProduct productService)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.imageService = imageService;
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files received.");
            }
            if (!int.TryParse(Request.Form["Id"], out int Id))
            {
                return BadRequest("Invalid Id.");
            }

            var productImages = new List<ImageModel>();
            var uploadsFolderPath = Path.Combine(hostingEnvironment.WebRootPath, "IMG");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            foreach (var item in files)
            {
                var filePath = Path.Combine(uploadsFolderPath, item.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await item.CopyToAsync(fileStream);
                }
                var productImage = new ImageModel
                {
                    ImageURL = $"\\IMG\\{item.FileName}",
                    ProductId = Id
                };
                productImages.Add(productImage);
            }
            await imageService.SetImagesAsync(productImages);
            return Ok();
        }

    }
}
