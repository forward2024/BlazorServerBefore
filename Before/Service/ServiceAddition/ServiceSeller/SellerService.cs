using Before.Data;
using Before.Data.Models;
using Before.Service.ServiceBlazor;
using Microsoft.EntityFrameworkCore;

namespace Before.Service.ServiceAddition.ServiceSeller
{
    public class SellerService : ISeller
    {
        private readonly DbContextOptions<ApplicationDbContext> context;
        private BlazorService blazorService;

        public SellerService(DbContextOptions<ApplicationDbContext> context, BlazorService blazorService)
        {
            this.context = context;
            this.blazorService = blazorService;
        }

        public async Task GetAllAsync()
        {
            using (var contextUsing = new ApplicationDbContext(context))
            {
                blazorService.Sellers = new HashSet<Seller>(await contextUsing.Sellers.ToListAsync());
            }
        }
    }
}
