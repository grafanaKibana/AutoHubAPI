﻿using AutoHub.BusinessLogic.Configuration;
using AutoHub.BusinessLogic.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Models;

namespace AutoHub.BusinessLogic.Services;

public class EmailService(IOptions<MailConfiguration> mailConfiguration) : IEmailService
{
    private readonly MailConfiguration _mailConfiguration = mailConfiguration.Value;

    public async Task SendEmail(SendMailRequest mailRequest)
    {
        var builder = new BodyBuilder
        {
            HtmlBody = mailRequest.Body
        };

        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailConfiguration.SenderMail),
            Subject = mailRequest.Subject,
            Body = builder.ToMessageBody(),
        };

        email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

        using var smtp = new SmtpClient();

        smtp.AuthenticationMechanisms.Remove("XOAUTH2");

        await smtp.ConnectAsync(_mailConfiguration.Host, _mailConfiguration.Port, true);
        await smtp.AuthenticateAsync(_mailConfiguration.SenderMail, _mailConfiguration.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}