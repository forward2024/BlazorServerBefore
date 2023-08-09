using Microsoft.AspNetCore.Identity;

namespace Before.Data.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
