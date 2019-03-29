using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WebMarket.Data.Entities.Security;
using WebMarket.Models.Security;

namespace WebMarket.Data.Repositories.Security
{
    public interface IRoleRepository
    {
        Role GetRole(Guid? id = null, string name = null);
        IEnumerable<Role> GetRoles(bool? isActive);
        void Update(Role role);
        void Insert(Role role);
        void Delete(Guid id);
    }

    public class RoleRepository : IRoleRepository
    {
        protected readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public Role GetRole(Guid? id = null, string name = null)
        {
            Expression<Func<RoleDB, bool>> lamda = null;

            if (id != null && name == null)
                lamda = x => x.Id == id;
            else if (id == null && name != null)
                lamda = x => x.Name == name;
            else
                throw new Exception("must have strictly one parameter");

            return (Role)_context.Roles.FirstOrDefault(lamda);
        }

        public IEnumerable<Role> GetRoles(bool? isActive)
        {
            return (isActive == null)
    ? _context.Roles.Select(x => (Role)x).ToList()
    : _context.Roles.Where(x => x.IsActive == isActive).Select(x => (Role)x).ToList();
        }

        public void Insert(Role role)
        {
            role.CreationDate = DateTime.Now;

            _context.Roles.Add((RoleDB)role);
            _context.SaveChanges();
        }

        public void Update(Role role)
        {
            var roleDb = _context.Roles.First(x => x.Id == role.Id);

            roleDb.Name = role.Name;
            roleDb.IsActive = role.IsActive;

            roleDb.UpdateTime = DateTime.Now;

            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var roleDb = _context.Roles.First(x => x.Id == id);

            _context.Roles.Remove(roleDb);
            _context.SaveChanges();
        }
    }
}
