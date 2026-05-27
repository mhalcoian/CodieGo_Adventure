using System.Net;
using System.Net.Mail;

namespace CodieGo_Adventure.Services
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailService(string host, int port, string user, string pass)
        {
            _smtpHost = host;
            _smtpPort = port;
            _smtpUser = user;
            _smtpPass = pass;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_smtpUser, "CODIE GO ADVENTURE");
            mailMessage.ReplyToList.Add(new MailAddress("no-reply@codiego.invalid"));
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;

            using (var client = new SmtpClient(_smtpHost, _smtpPort))
            {
                client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                client.EnableSsl = true;

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
