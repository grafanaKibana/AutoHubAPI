namespace AutoHub.Domain.Exceptions;

public class InvalidValueException : Exception
{
    public InvalidValueException()
    {
    }

    public InvalidValueException(string errorMessage) : base(errorMessage)
    {
    }
}