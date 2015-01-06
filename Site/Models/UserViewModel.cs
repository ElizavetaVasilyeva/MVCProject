using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BLL.Interfaces;

namespace Site.Models
{
    public class UserViewModel
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
        public string Role { get; set; }

    }
}