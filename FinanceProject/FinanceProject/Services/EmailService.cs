using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace FinanceProject.Services
{
    public class EmailsServices
    {
        private readonly IConfiguration _configuration;

        public EmailsServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["Email:UserName"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = body
            };

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["Email:Host"], 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["Email:UserName"], _configuration["Email:Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
