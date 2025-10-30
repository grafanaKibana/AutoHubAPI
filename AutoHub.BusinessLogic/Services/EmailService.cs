using System.Threading.Tasks;
using AutoHub.BusinessLogic.Configuration;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.BusinessLogic.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AutoHub.BusinessLogic.Services;

public class EmailService(IOptions<MailConfiguration> mailConfiguration) : IEmailService
{
    private readonly MailConfiguration mailConfiguration = mailConfiguration.Value;

    public async Task SendEmail(SendMailRequest mailRequest)
    {
        var builder = new BodyBuilder
        {
            HtmlBody = mailRequest.Body
        };

        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(mailConfiguration.SenderMail),
            Subject = mailRequest.Subject,
            Body = builder.ToMessageBody(),
        };

        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

        using var smtp = new SmtpClient();

        smtp.AuthenticationMechanisms.Remove("XOAUTH2");

        await smtp.ConnectAsync(mailConfiguration.Host, mailConfiguration.Port, true);
        await smtp.AuthenticateAsync(mailConfiguration.SenderMail, mailConfiguration.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}