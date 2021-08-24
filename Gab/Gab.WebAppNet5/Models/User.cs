using Microsoft.AspNetCore.Identity;

namespace Gab.WebAppNet5.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }

        // public ICollection<Post> Posts { get; set; }
    }
}
