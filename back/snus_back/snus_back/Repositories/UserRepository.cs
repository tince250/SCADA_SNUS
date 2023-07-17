using snus_back.data_access;
using snus_back.Models;

namespace snus_back.Repositories
{
    public class UserRepository
    {
        private SNUSDbContext dbContext;

        public UserRepository(SNUSDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public User? getByUsername(string username)
        {
            return dbContext.Users.FirstOrDefault(user => user.Username == username);
        }
    }
}
