using System;

namespace AutoHub.BLL.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException()
        {
        }

        public LoginFailedException(string errorMessage) : base(errorMessage)
        {
        }
    }
}