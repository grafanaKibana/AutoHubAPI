namespace AutoHub.BusinessLogic.Configuration;

public record MailConfiguration
{
    public string SenderMail { get; set; }

    public string DisplayName { get; set; }

    public string Password { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }
}
