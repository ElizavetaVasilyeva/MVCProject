using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class CommentViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
    }
}