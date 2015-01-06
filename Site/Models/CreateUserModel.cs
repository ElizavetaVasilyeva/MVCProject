using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class CreateUserModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter your login")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Value must be between 5 to 10 characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please, enter your e-mail")]
        [EmailAddress(ErrorMessage = "Incorrect e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Value must be between 3 to 15 characters")]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        public string Role { get; set; }
    }
}