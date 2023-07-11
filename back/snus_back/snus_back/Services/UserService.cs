using snus_back.DTOs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Services
{
    public class UserService : IUserService
    {
        private UserRepository allUsers;

        public UserService(UserRepository allUsers)
        {
            this.allUsers = allUsers;
        }

        public User Login(CredentialsDTO creds)
        {
            User? foundUser = this.allUsers.getByUsername(creds.Username);

            if (foundUser == null || foundUser.Password != creds.Password)
            {
                throw new Exception("Username or passowrd incorrect.");
            }

            return foundUser;
        }
    }
}
