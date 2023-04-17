using Before.Data.Models;
using Before.Service.ServiceAddition.ServiceCategory;
using Before.Service.ServiceBlazor;

namespace Before.Service.ServiceFillter
{
    public class FillterService : IFillter
    {
        private readonly BlazorService blazorService;

        public FillterService(BlazorService blazorService)
        {
            this.blazorService = blazorService;
        }

        public HashSet<string> SelectedTypeItem { get; set; } = new HashSet<string>();
        public HashSet<string> SelectedCategory { get; set; } = new HashSet<string>();
        public HashSet<string> SelectedColor { get; set; } = new HashSet<string>();
        public HashSet<string> SelectedSeason { get; set; } = new HashSet<string>();
        public HashSet<string> SelectedSize { get; set; } = new HashSet<string>();

        /*public List<Product> GetFilteredProducts(List<Product> products)
        {
            return products.Where(p => (!SelectedTypeItem.Any() || SelectedTypeItem.Contains(p.TypeItem.Name))
                                       && (!SelectedCategory.Any() || SelectedCategory.Contains(p.Category.Name))
                                       && (!SelectedColor.Any() || SelectedColor.Contains(p.Color.Name))
                                       && (!SelectedSeason.Any() || p.ProductSeasons.Any(ps => SelectedSeason.Contains(ps.Season.Name)))
                                       && (!SelectedSize.Any() || p.Sizes.Any(sp => SelectedSize.Contains(sp.Size.Name)))).ToList();
        }*/

        public async Task FillTypeItems(string typeitem, bool value)
        {
            if (value) SelectedTypeItem.Add(typeitem);
            else SelectedTypeItem.Remove(typeitem);
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
    }
}
