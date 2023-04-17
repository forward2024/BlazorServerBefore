namespace Before.Data.Models
{
    public class SeasonModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; }
    }
}
