namespace Before.Data.Models
{
    public class TypeItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public TypeItem() { }
        public TypeItem(TypeItem other)
        {
            this.Name = other.Name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            TypeItem other = (TypeItem)obj;
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
