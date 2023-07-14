namespace Before.Data.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Size() { }
        public Size(Size other)
        {
            this.Name = other.Name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Size other = (Size)obj;
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
