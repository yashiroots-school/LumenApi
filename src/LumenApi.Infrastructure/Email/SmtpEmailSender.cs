using System.Net;
using System.Net.Mail;
using LumenApi.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace LumenApi.Infrastructure.Email;

public class SmtpEmailSender(ILogger<SmtpEmailSender> _logger) : IEmailSender
{
  public async Task SendEmailAsync(string to, string from, string subject, string body)
  {
    try
    {
      NetworkCredential nc = new NetworkCredential("pranaybansod59@gmail.com", "lppasizpglpkdtca");
      var emailClient = new SmtpClient
      {
        EnableSsl = true,
        Host = "smtp.gmail.com",
        Port = 587,
        UseDefaultCredentials = false,
        Credentials = nc
      }; // TODO: pull settings from config
      emailClient.EnableSsl = true;
      var message = new MailMessage
      {
        From = new MailAddress(from),
        Subject = subject,
        Body = body
      };
      message.To.Add(new MailAddress("akpal421212@outlook.com"));
      await emailClient.SendMailAsync(message);
      _logger.LogWarning("Sending email to {to} from {from} with subject {subject}.", to, from, subject);
    }
    catch (Exception)
    {
    }
  }
}
