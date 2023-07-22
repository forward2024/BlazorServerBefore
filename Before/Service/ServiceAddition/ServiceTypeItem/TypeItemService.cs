using Before.Data;
using Before.Data.Models;
using Before.Service.ServiceBlazor;
using Microsoft.EntityFrameworkCore;

namespace Before.Service.ServiceAddition.ServiceTypeItem
{
    public class TypeItemService : ITypeItem
    {
        private readonly DbContextOptions<ApplicationDbContext> context;
        private BlazorService blazorService;

        public TypeItemService(DbContextOptions<ApplicationDbContext> context, BlazorService blazorService)
        {
            this.context = context;
            this.blazorService = blazorService;
        }

        public async Task GetAllAsync()
        {
            using (var contextUsing = new ApplicationDbContext(context))
            {
                blazorService.TypeItems = new HashSet<TypeItem>(await contextUsing.TypeItems.ToListAsync());
            }
        }
    }
}
