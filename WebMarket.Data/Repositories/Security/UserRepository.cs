using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WebMarket.Data.Entities.Security;
using WebMarket.Models.Security;

namespace WebMarket.Data.Repositories.Security
{
    public interface IUserRepository
    {
        User GetUser(Guid? id = null, string name = null, string email = null);
        IEnumerable<User> GetUsers(bool? isActive);
        void Update(User user);
        void Insert(User user);
        void Delete(Guid id);
        void ChangePassword(Guid id, string password);
    }

    public class UserRepository : IUserRepository
    {
        protected readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User GetUser(Guid? id = null, string UserName = null, string email = null)
        {
            Expression<Func<UserDB, bool>> lamda = null;

            if (id != null && UserName == null && email == null)
                lamda = x => x.Id == id;
            else if (id == null && UserName != null && email == null)
                lamda = x => x.UserName == UserName;
            else if (id == null && UserName == null && email != null)
                lamda = x => x.Email == email;
            else if (id == null && UserName == null && email == null)
                throw new Exception("Expect at least on parameter");
            else
                throw new Exception("Two or more parameter, expect only one");

            var userDb = _context.Users.FirstOrDefault(lamda);

            return (User)_context.Users.FirstOrDefault(lamda);
        }

        public IEnumerable<User> GetUsers(bool? isActive = null)
        {
            return (isActive == null)
                ? _context.Users.Select(x => (User)x).ToList()
                : _context.Users.Where(x => x.IsActive == isActive).Select(x => (User)x).ToList();
        }
        public void Insert(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreationDate = DateTime.Now;

            _context.Users.Add((UserDB)user);
            _context.SaveChanges();
        }
        public void Update(User user)
        {
            var userDb = _context.Users.First(x => x.Id == user.Id);

            userDb.UserName = user.UserName;
            userDb.Email = user.Email;
            userDb.UserRoles = ((UserDB)user).UserRoles;
            
            userDb.UpdateTime = DateTime.Now;

            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var userDb = _context.Users.First(x => x.Id == id);

            _context.Users.Remove(userDb);
            _context.SaveChanges();

        }

        public void ChangePassword(Guid id, string password)
        {
            var userDb = _context.Users.First(x => x.Id == id);

            userDb.Password = password;

            userDb.UpdateTime = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
