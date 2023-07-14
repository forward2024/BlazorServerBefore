namespace Before.Data.Models
{
    public class Season
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Season(){ }
        public Season(Season other)
        {
            this.Name = other.Name;
        }
    }
}
