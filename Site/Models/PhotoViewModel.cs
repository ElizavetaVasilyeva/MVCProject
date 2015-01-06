using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Models
{
    public class PhotoViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [ScaffoldColumn(false)]
        public string ImageType { get; set; }

        public DateTime CreatedDate { get; set; }

        public string TagName { get; set; }

    }
}