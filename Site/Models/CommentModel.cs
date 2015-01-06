using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Site.Models
{
    public class CommentModel
    {
        [HiddenInput(DisplayValue =false )]
        public int IdPhoto { get; set; }
         [HiddenInput(DisplayValue = false)]
        public string Description { get; set; }
         [HiddenInput(DisplayValue = false)]
        public DateTime CreatedDatePhoto { get; set; }
         [HiddenInput(DisplayValue = false)]
        public string Image { get; set; }
        public string Text { get; set; }
         [HiddenInput(DisplayValue = false)]
        public string PhotoUser { get; set; }
         [HiddenInput(DisplayValue = false)]
        public string CurrentUserName { get; set; }

    }
}