namespace Before.Data.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Seller() { }
        public Seller(Seller other)
        {
            this.Name = other.Name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Seller other = (Seller)obj;
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
