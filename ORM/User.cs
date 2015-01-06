namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            Albums = new HashSet<Album>();
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Profiles = new HashSet<Profile>();
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public int RoleId { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }

        public virtual Role Role { get; set; }
    }
}
