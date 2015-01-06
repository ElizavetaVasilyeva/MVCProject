namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Album
    {
        public Album()
        {
            Photos = new HashSet<Photo>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
