using Before.Data.Models;

namespace Before.Service.ServiceAddition.ServiceImage
{
    public interface IImage
    {
        Task GetAllAsync();
        Task SetImagesAsync(List<ImageModel> fileNames);
        Task DeleteImage(int Id);
        Task SwapProductImagesAsync(int firstId, int secondId);
    }
}
