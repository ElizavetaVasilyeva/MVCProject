using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.Exseptions
{
    public class UserServiceException : Exception
    {
        public UserServiceException(Exception ex)
            : base("UserService exception"+ex.Message, ex)
        {

        }

        public UserServiceException(string message)
            : base(message)
        {

        }

        public UserServiceException()
        {
            
        }
    }
}
