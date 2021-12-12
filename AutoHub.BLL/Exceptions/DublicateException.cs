using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHub.BLL.Exceptions
{
    public class DublicateException : Exception
    {
        public DublicateException()
        {
        }

        public DublicateException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
