namespace AutoHub.BusinessLogic.Models;

public record SendMailRequest
{
    public string ToEmail { get; init; }

    public string Subject { get; init; }

    public string Body { get; init; }
}
