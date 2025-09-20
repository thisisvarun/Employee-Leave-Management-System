using backend.Service.Interfaces;
using System.Net.Mail;
using System.Net;

namespace backend.Service
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(string toAddress, string subject, string body)
        {
            string _smtphost = "smtp.gmail.com";
            int _smtpport = 587;
            string _smtpuser = "harish.16634@gmail.com";
            string _smtppass = "gndn jznv rdqk iaqi";

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
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
