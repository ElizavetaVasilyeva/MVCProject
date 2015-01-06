using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Exseptions
{
    public class UserNotFoundException : UserServiceException
    {
        public int Id { get; set; }
    }
}
