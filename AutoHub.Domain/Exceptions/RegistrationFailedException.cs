namespace AutoHub.Domain.Exceptions;

public class RegistrationFailedException : Exception
{
    public RegistrationFailedException()
    {
    }

    public RegistrationFailedException(string errorMessage) : base(errorMessage)
    {
    }
}