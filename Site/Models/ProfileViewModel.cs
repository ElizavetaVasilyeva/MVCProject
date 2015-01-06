using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class ProfileViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Display(Name="First name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Value must be between 3 to 15 characters")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Value must be between 3 to 15 characters")]
        public string LastName { get; set; }
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Incorrect e-mail")]
        public string Email { get; set; }
         [Display(Name = "Homecountry")]
        public string Country { get; set; }
        [Display(Name = "Hometown")]
        public string City { get; set; }
        [Display(Name = "Mobile")]
        public string PhoneNumber { get; set; }
        [Display(Name = "About me")]
        public string AboutYourself { get; set; }
        [Display(Name = "My interests")]
        public string Interests { get; set; }
        [Display(Name = "Last update")]
        public DateTime LastUpdateDate { get; set; }
          
    }
}