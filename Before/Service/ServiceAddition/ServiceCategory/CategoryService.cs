using Before.Data;
using Before.Data.Models;
using Before.Service.ServiceBlazor;
using Microsoft.EntityFrameworkCore;

namespace Before.Service.ServiceAddition.ServiceCategory
{
    public class CategoryService : ICategory
    {
        private readonly DbContextOptions<ApplicationDbContext> context;
        private BlazorService blazorService;

        public CategoryService(DbContextOptions<ApplicationDbContext> context, BlazorService blazorService)
        {
            this.context = context;
            this.blazorService = blazorService;
        }

        public async Task GetAllAsync()
        {
            using (var contextUsing = new ApplicationDbContext(context))
            {
                blazorService.Categories = new HashSet<Category>(await contextUsing.Categories.ToListAsync());
            }
        }
    }
}
