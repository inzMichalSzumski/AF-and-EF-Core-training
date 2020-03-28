using Microsoft.EntityFrameworkCore;
using System.Linq;
using UserCase.Models;

namespace UserCase.Services
{
    public class DbService : IDbService
    {
        private readonly userscaseContext context;

        public DbService(userscaseContext userscaseContext)
        {
            context = userscaseContext;
        }

        public User CreateUser(User user)
        {
            this.context.User.Add(user);
            context.SaveChanges();
            return user;
        }

        public void DeleteUser(long userId)
        {
            var userToDelete = this.context.User.SingleOrDefault(x => x.Id == userId);
            this.context.User.Remove(userToDelete);
            context.SaveChanges();
        }

        public User GetUser(long userId)
        {
            return this.context.User.AsNoTracking().SingleOrDefault(x => x.Id == userId);
        }

        public User UpdateUser(User user)
        {
            var userToUpdate = this.context.User.SingleOrDefault(x => x.Id == user.Id);

            if (userToUpdate != null)
            {
                userToUpdate.Initials = user.Initials;
                userToUpdate.Name = user.Name;
                this.context.SaveChanges();
                return userToUpdate;
            }

            return null;
        }
    }
}
