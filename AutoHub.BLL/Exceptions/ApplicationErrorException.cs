using System;

namespace AutoHub.BLL.Exceptions
{
    public class ApplicationErrorException : Exception
    {
        public ApplicationErrorException()
        {
        }

        public ApplicationErrorException(string errorMessage) : base(errorMessage)
        {
        }

        // public ApplicationErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        // { }
    }
}