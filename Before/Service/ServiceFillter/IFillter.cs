namespace Before.Service.ServiceFillter
{
    public interface IFillter
    {
        Task FillTypeItems(string color, bool value);
        Task FillCategory(string color, bool value);
        Task FillColor(string color, bool value);
        Task FillSeason(string color, bool value);
        Task FillSize(string color, bool value);
    }
}
