using System;

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