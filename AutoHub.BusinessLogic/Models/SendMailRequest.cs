namespace AutoHub.Domain.Entities;

public class SendMailRequest
{
    public string ToEmail { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }
}
