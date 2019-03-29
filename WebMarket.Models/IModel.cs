using System;
using System.Collections.Generic;
using System.Text;

namespace WebMarket.Models
{
    interface IModel
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreationDate { get; set; }
        DateTime? UpdateTime { get; set; }
    }
}
