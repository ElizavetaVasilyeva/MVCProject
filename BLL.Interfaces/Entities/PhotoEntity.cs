using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public class PhotoEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AlbumId { get; set; }

        public int? TagId { get; set; }
    }
}
