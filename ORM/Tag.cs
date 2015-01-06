namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tag
    {
        public Tag()
        {
            Photos = new HashSet<Photo>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
