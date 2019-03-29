using System;
using System.Collections.Generic;

namespace WebMarket.Data.Entities.Security
{
    public partial class UserRoleDB
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual RoleDB Role { get; set; }
        public virtual UserDB User { get; set; }
    }
}
