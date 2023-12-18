using Microsoft.EntityFrameworkCore;

namespace Entities.Users
{
    [Index(nameof(User.Name), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Tel { get; set; }

        public string? Role { get; set; } 
    }
}
