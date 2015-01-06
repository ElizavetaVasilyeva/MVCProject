using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Exseptions
{
    public class AlbumServiceException:Exception
    {
        public AlbumServiceException(Exception ex):base("AlbumService exception " + ex.Message,ex)
        {
            
        }

        public AlbumServiceException(string message):base(message)
        {
            
        }

        public AlbumServiceException()
        {
            
        }
    }
}
