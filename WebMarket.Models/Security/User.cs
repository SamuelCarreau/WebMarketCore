using System;
using System.Collections.Generic;
using System.Text;

namespace WebMarket.Models.Security
{
    public class User : IModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
