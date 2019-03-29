using System;
using System.Collections.Generic;
using System.Text;

namespace WebMarket.Models.Security
{
    public class Role : IModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
