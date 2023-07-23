using Before.Data.Models;

namespace Before.Service.ServiceFillter
{
    public interface IFilter
    {
        bool activeFilter();
        void ClearFilters();
        List<Product> Products { get; }
        Task FillSeason(string color, bool value);
        Task FillSize(string color, bool value);
        Task FillCategory(string color, bool value);
        Task FillColor(string color, bool value);
    }
}
