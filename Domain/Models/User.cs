using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public string PhotoPath { get; set; }

        public List<Movie> OldMovie { get; set; }
    }
}
