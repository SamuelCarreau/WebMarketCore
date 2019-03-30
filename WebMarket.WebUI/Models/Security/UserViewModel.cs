using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Models.Security;

namespace WebMarket.WebUI.Models.Security
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
