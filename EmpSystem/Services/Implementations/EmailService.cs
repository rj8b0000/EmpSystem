using System.Net;
using System.Net.Mail;
using EmpSystem.Models;
using EmpSystem.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EmpSystem.Services.Implementations;

public class EmailService: IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
        {
            Port = _emailSettings.Port,
            Credentials = new NetworkCredential(
                _emailSettings.SenderEmail,
                _emailSettings.Password
            ),
            EnableSsl = true
        };

        var message = new MailMessage
        {
            From = new MailAddress(
                _emailSettings.SenderEmail,
                _emailSettings.SenderName
            ),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        message.To.Add(toEmail);
        
        await smtpClient.SendMailAsync(message);
    }
}