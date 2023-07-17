using System.ComponentModel.DataAnnotations;

namespace snus_back.Models
{
    public enum UserRole
    {
        USER,
        ADMIN
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public UserRole Role { get; set; }
    }
}
