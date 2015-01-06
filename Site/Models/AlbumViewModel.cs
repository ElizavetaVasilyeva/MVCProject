using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class AlbumViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Album title")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Value must be between 5 to 10 characters")]
        public string Name { get; set; }

        [StringLength(350, MinimumLength = 0, ErrorMessage = "Value must be between 0 to 350 characters")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
 
    }
}