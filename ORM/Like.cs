namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Like
    {
        public int Id { get; set; }

        public DateTime SetDate { get; set; }

        public int UserId { get; set; }

        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; }

        public virtual User User { get; set; }
    }
}
