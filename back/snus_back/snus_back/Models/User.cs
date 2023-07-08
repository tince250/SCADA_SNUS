using System.ComponentModel.DataAnnotations;

namespace snus_back.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
