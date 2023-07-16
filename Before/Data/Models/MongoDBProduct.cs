using System.ComponentModel.DataAnnotations.Schema;

namespace Before.Data.Models
{
    public class MongoDBProduct
    {
        public int Id { get; set; }
        public List<byte[]> Images { get; set; } = new List<byte[]>();
        public HashSet<string> Seasons { get; set; } = new HashSet<string>();
        public HashSet<string> Sizes { get; set; } = new HashSet<string>();
        public HashSet<string> Colors { get; set; } = new HashSet<string>();
    }
}
