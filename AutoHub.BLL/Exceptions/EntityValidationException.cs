using System;

namespace AutoHub.BLL.Exceptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException()
        {
        }

        public EntityValidationException(string errorMessage) : base(errorMessage)
        {
        }
    }
}