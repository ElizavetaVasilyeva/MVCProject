using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DTO
{
    public class DalProfile:IEntity
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
    }
}
