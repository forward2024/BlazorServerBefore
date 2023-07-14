namespace Before.Data.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Color() { }
        public Color(Color other)
        {
            this.Id = other.Id;
            this.Name = other.Name;
        }
    }
}
