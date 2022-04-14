using AutoHub.BusinessLogic.Configuration;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SmtpClient = System.Net.Mail.SmtpClient;

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
        //var builder = new BodyBuilder
        //{
        //    HtmlBody = mailRequest.Body
        //};

        //var email = new MimeMessage
        //{
        //    Sender = MailboxAddress.Parse(_mailConfiguration.Mail),
        //    Subject = mailRequest.Subject,
        //    Body = builder.ToMessageBody(),
        //};

        //email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

        //using var smtp = new SmtpClient();

        //smtp.AuthenticationMechanisms.Remove("XOAUTH2");

        //await smtp.ConnectAsync(_mailConfiguration.Host, _mailConfiguration.Port, true);
        //await smtp.AuthenticateAsync(_mailConfiguration.Mail, _mailConfiguration.Password);
        //await smtp.SendAsync(email);
        //await smtp.DisconnectAsync(true);

        using var message = new MailMessage(_mailConfiguration.Mail, mailRequest.ToEmail);

        message.Subject = mailRequest.Subject;
        message.Body = mailRequest.Body;

        message.IsBodyHtml = false;
        using SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = true;
        smtp.Credentials = new NetworkCredential(_mailConfiguration.Mail, _mailConfiguration.Password);
        smtp.Port = _mailConfiguration.Port;
        smtp.Send(message);
    }
}