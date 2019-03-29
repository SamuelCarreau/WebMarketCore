using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMarket.WebUI.Models
{
    public class RoleViewModel
    {
        private const string REQUIRED_ERROR_MESSAGE = "Is required";
        private const string NAME_REGEX = @"^[a-zA-Z\s]+$";

        public Guid Id { get; set; }
        [Required(ErrorMessage = REQUIRED_ERROR_MESSAGE)]
        [MinLength(5), MaxLength(25), RegularExpression(NAME_REGEX), DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
