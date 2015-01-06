using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Site.Models
{
    public class NewPhotoModel
    {
        public int Album { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string Description { get; set; }

    }
}