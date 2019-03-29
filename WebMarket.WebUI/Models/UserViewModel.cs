using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMarket.Models.Security;

namespace WebMarket.WebUI.Models
{
    public class UserViewModel
    {
        private const string EMAIL_REGEX = @"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        private const string NAME_REGEX = @"^[a-zA-Z\s]+$";
        private const string REQUIRED_ERROR_MESSAGE = "Is required";

        public Guid Id { get; set; }

        [Required(ErrorMessage = REQUIRED_ERROR_MESSAGE)]
        [MinLength(5), MaxLength(25), RegularExpression(NAME_REGEX), DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = REQUIRED_ERROR_MESSAGE)]
        [MinLength(5), MaxLength(25),RegularExpression(EMAIL_REGEX), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MinLength(8), MaxLength(50), DataType(DataType.Password)]
        public string Password { get; set; }

        [MinLength(8), MaxLength(50), DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepeatPassword { get; set; }

        public List<Role> Roles { get; set; }
    }
}
