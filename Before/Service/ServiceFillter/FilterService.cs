using Before.Data.Models;
using Before.Service.ServiceProduct;
using System.Linq;

namespace Before.Service.ServiceFillter
{
    public class FilterService : IFilter
    {
        private readonly BlazorService blazorService;

        public FilterService(BlazorService blazorService)
        {
            this.blazorService = blazorService;
        }
        public HashSet<string> SelectedCategory { get; set; } = new HashSet<string>();
        public HashSet<string> SelectedColor { get; set; } = new HashSet<string>();
        public HashSet<string> SelectedSeason { get; set; } = new HashSet<string>();
        public HashSet<string> SelectedSize { get; set; } = new HashSet<string>();
        public List<Product> Products { get; private set; } = new List<Product>();

        public bool activeFilter()
        {
            if (SelectedCategory.Any() || SelectedColor.Any() || SelectedSeason.Any() || SelectedSize.Any())
            {
                GetFilteredProducts();
                return true;
            }
            Products = new List<Product>();
            return false;
        }

        private void GetFilteredProducts()
        {
            Products = blazorService.Products
                .Where(p => (!SelectedCategory.Any() || SelectedCategory.Contains(p.Category.Name)) &&
                            (!SelectedColor.Any() || SelectedColor.Intersect(p.Colors).Any()) &&
                            (!SelectedSeason.Any() || SelectedSeason.Intersect(p.Seasons).Any()) &&
                            (!SelectedSize.Any() || SelectedSize.Intersect(p.Sizes).Any())).ToList();
        }


        public async Task FillCategory(string category, bool value)
        {
            if (value) SelectedCategory.Add(category);
            else SelectedCategory.Remove(category);
        }

        public async Task FillColor(string color, bool value)
        {
            if (value) SelectedColor.Add(color);
            else SelectedColor.Remove(color);
        }

        public async Task FillSeason(string season, bool value)
        {
            if (value) SelectedSeason.Add(season);
            else SelectedSeason.Remove(season);
        }

        public async Task FillSize(string size, bool value)
        {
            if (value) SelectedSize.Add(size);
            else SelectedSize.Remove(size);
        }

        public void ClearFilters()
        {
            SelectedCategory = new HashSet<string>();
            SelectedColor = new HashSet<string>();
            SelectedSeason = new HashSet<string>();
            SelectedSize = new HashSet<string>();
            Products = new List<Product>();
        }
    }
}
