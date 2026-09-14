namespace AutoHub.BusinessLogic.Configuration;

public record MailConfiguration
{
    public string SenderMail { get; init; }

    public string DisplayName { get; init; }

    public string Password { get; init; }

    public string Host { get; init; }

    public int Port { get; init; }
}
