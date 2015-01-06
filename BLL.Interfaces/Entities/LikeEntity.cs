using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class LikeEntity
    {
        public int Id { get; set; }

        public DateTime SetDate { get; set; }

        public int UserId { get; set; }

        public int PhotoId { get; set; }

    }
}
