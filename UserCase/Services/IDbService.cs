using UserCase.Models;

namespace UserCase.Services
{
    public interface IDbService
    {
        public User CreateUser(User user);
        public void DeleteUser(long userId);
        public User GetUser(long userId);
        public User UpdateUser(User user);
    }
}
