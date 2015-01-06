using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Exseptions
{
    public class DuplicateLoginException:UserServiceException
    {
        public string Login { get; set; }
    }
}
