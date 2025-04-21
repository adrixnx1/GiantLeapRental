using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace GiantLeapRental.Services
{
    public class EmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public void Send(string toEmail, string subject, string body)
        {
            var smtp = _config.GetSection("Smtp");

            var fromEmail = smtp["FromEmail"];
            var password = smtp["Password"];
            var host = smtp["Host"];
            var port = int.Parse(smtp["Port"]);
            var enableSsl = bool.Parse(smtp["EnableSsl"]);

            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = enableSsl
            };

            var message = new MailMessage(fromEmail, toEmail, subject, body)
            {
                IsBodyHtml = false
            };

            client.Send(message);
        }
    }
}

