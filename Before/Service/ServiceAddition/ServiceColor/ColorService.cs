using Before.Data;
using Before.Service.ServiceBlazor;
using Microsoft.EntityFrameworkCore;

namespace Before.Service.ServiceAddition.ServiceColor
{
    public class ColorService : IColor
    {
        private readonly DbContextOptions<ApplicationDbContext> context;
        private BlazorService blazorService;

        public ColorService(DbContextOptions<ApplicationDbContext> context, BlazorService blazorService)
        {
            this.context = context;
            this.blazorService = blazorService;
        }

        public async Task GetAllAsync()
        {
            using (var contextUsing = new ApplicationDbContext(context))
            {
                blazorService.Colors = await contextUsing.Colors.ToListAsync();
            }
        }
    }
}
