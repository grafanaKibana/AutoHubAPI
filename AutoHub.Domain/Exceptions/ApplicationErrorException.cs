namespace AutoHub.Domain.Exceptions;

public class ApplicationErrorException : Exception
{
    public ApplicationErrorException()
    {
    }

    public ApplicationErrorException(string errorMessage) : base(errorMessage)
    {
    }
}