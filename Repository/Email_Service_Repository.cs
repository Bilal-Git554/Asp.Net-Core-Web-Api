using Library_Management.Entities;
using Library_Management.IRepository;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Library_Management.Repository
{
    public class Email_Service_Repository : IEmail_Service_Repository
    {
        private readonly IConfiguration _configuration;
        public Email_Service_Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Send_Email(Email_Service email_with_to_body_subject)
        {
            var host = _configuration["SMTP:Host"];
            var port = int.Parse(_configuration["SMTP:Port"]);
            var username = _configuration["SMTP:Username"];
            var password = _configuration["SMTP:Password"];

            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(username));
            message.To.Add(MailboxAddress.Parse(email_with_to_body_subject.To));
            message.Subject = email_with_to_body_subject.Subject;

            message.Body = new TextPart("plain")
            {
                Text = email_with_to_body_subject.Body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);

            return;
        }
    }
}
