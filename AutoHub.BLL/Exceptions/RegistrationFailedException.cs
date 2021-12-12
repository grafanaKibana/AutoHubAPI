using System;

namespace AutoHub.BLL.Exceptions
{
    public class RegistrationFailedException : Exception
    {
        public RegistrationFailedException()
        {
        }

        public RegistrationFailedException(string errorMessage) : base(errorMessage)
        {
        }
    }
}