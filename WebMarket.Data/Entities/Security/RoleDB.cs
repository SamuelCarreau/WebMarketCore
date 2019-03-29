using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebMarket.Models;
using WebMarket.Models.Security;

namespace WebMarket.Data.Entities.Security
{
    public class RoleDB : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public virtual ICollection<UserRoleDB> UserRole { get; set; }

        public static explicit operator RoleDB(Role role)
        {
            return role != null
                ? new RoleDB
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsActive = role.IsActive,
                    CreationDate = role.CreationDate,
                    UpdateTime = role.UpdateTime,
                }
                : null;
        }

        public static explicit operator Role(RoleDB roleDb)
        {
            return roleDb != null
                ? new Role
                {
                    Id = roleDb.Id,
                    Name = roleDb.Name,
                    IsActive = roleDb.IsActive,
                    CreationDate = roleDb.CreationDate,
                    UpdateTime = roleDb.UpdateTime
                }
                : null;
        }
    }
}
