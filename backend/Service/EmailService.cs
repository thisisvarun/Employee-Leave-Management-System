using backend.Service.Interfaces;
using System.Net.Mail;
using System.Net;

namespace backend.Service
{
    public class EmailService : IEmailService
    {
        public readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmailAsync(string toAddress, string subject, string body)
        {
            string _smtphost = _config["EmailEnvVars:SmtpHost"]!;
            int _smtpport = int.Parse(_config["EmailEnvVars:SmtpPort"]!);
            string _smtpuser = _config["EmailEnvVars:SmtpUser"]!;
            string _smtppass = _config["EmailEnvVars:SmtpPass"]!;

            using var message = new MailMessage();
            message.From = new MailAddress(_smtpuser);
            message.To.Add(toAddress);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using var client = new SmtpClient(_smtphost, _smtpport)
            {
                Credentials = new NetworkCredential(_smtpuser, _smtppass),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
