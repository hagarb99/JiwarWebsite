using System.Net;
using System.Net.Mail;

namespace Jiwar.Services.MailService
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMailAsync(string emailTo, string subject, string body)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_config["Mail:From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(emailTo);

            var client = new SmtpClient(_config["Mail:Host"], int.Parse(_config["Mail:Port"]))
            {
                Credentials = new NetworkCredential(
                    _config["Mail:Username"],
                    _config["Mail:Password"]
                ),
                EnableSsl = true
            };

            await client.SendMailAsync(mail);
        }

    }
}
