
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using ContactManagerDataBusiness.Interfaces;

namespace ContactManagerDataBusiness.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;

        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            _logger = logger;
        }

        public void SendEmailNotification(Guid contactId)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress("noreply", "noreply@contactmanager.com"));
                message.To.Add(new MailboxAddress("SysAdmin", "Admin@contactmanager.com"));
                message.Subject = "ContactManager System Alert";

                message.Body = new TextPart("plain")
                {
                    Text = "Contact with id:" + contactId.ToString() + " was updated"
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("127.0.0.1", 25, false);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while sending an email in {nameof(EmailNotificationService)}.{nameof(SendEmailNotification)}, Message: {ex.Message}");
            }
        }
    }
}
