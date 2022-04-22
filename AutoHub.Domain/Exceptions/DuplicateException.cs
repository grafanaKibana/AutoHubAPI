namespace AutoHub.Domain.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException()
    {
    }

    public DuplicateException(string errorMessage) : base(errorMessage)
    {
    }
}