using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public static class CheckForNull
    {
        public static void ArgumentisNull(this object value)
        {
            if (value==null) throw new ArgumentNullException();
        }
    }
}
