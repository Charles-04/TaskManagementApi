using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BLL.Extensions
{
    public static class NullCheckExtension
    {
        public static void CheckNull(this object obj, string message )
        {
            if(obj == null )
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}
