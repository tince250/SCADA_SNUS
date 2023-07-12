using snus_back.DTOs;
using snus_back.Models;

namespace snus_back.Services.ServiceInterfaces
{
    public interface IUserService
    {
        public User Login(CredentialsDTO creds);
    }
}
