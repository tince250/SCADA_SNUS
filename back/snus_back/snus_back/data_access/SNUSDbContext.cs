using Microsoft.EntityFrameworkCore;
using snus_back.Models;

namespace snus_back.data_access
{
    public class SNUSDbContext : DbContext
    {
        public SNUSDbContext(DbContextOptions<SNUSDbContext> options) : base(options) { }

        public DbSet<User> Users { get;set; }

    }
}
