using snus_back.Models;
using System.Data.Entity;

namespace snus_back.data_access
{
    public class SNUSDbContext : DbContext
    {
        public DbSet<User> Users { get;set; }

    }
}
