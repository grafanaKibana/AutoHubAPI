namespace AutoHub.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string errorMessage) : base(errorMessage)
    {
    }
}