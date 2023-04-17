using Before.Data;
using Before.Service.ServiceBlazor;
using Microsoft.EntityFrameworkCore;

namespace Before.Service.ServiceAddition.ServiceSize
{
    public class SizeService : ISize
    {
        private readonly DbContextOptions<ApplicationDbContext> context;
        private BlazorService blazorService;

        public SizeService(DbContextOptions<ApplicationDbContext> context, BlazorService blazorService)
        {
            this.context = context;
            this.blazorService = blazorService;
        }

        public async Task GetAllAsync()
        {
            using (var contextUsing = new ApplicationDbContext(context))
            {
                blazorService.Sizes = await contextUsing.Sizes.ToListAsync();
            }
        }
    }
}
