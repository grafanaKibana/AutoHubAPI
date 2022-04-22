using AutoHub.BusinessLogic.Configuration;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.BusinessLogic.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace AutoHub.BusinessLogic.Services;

public class EmailService : IEmailService
{
    private readonly MailConfiguration _mailConfiguration;

    public EmailService(IOptions<MailConfiguration> mailConfiguration)
    {
        _mailConfiguration = mailConfiguration.Value;
    }

    public async Task SendEmail(SendMailRequest mailRequest)
    {
        var builder = new BodyBuilder
        {
            HtmlBody = mailRequest.Body
        };

        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailConfiguration.Mail),
            Subject = mailRequest.Subject,
            Body = builder.ToMessageBody(),
        };

        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

        using var smtp = new SmtpClient();

        smtp.AuthenticationMechanisms.Remove("XOAUTH2");

        await smtp.ConnectAsync(_mailConfiguration.Host, _mailConfiguration.Port, true);
        await smtp.AuthenticateAsync(_mailConfiguration.Mail, _mailConfiguration.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}