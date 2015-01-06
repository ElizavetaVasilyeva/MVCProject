namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Photo
    {
        public Photo()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AlbumId { get; set; }

        public int? TagId { get; set; }

        public virtual Album Album { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
