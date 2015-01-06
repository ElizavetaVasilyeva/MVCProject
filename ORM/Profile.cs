namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Profile
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string AboutYourself { get; set; }

        public string Interests { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
