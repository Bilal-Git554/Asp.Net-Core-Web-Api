using Library_Management.Entities;
using Library_Management.IRepository;
using MailKit.Net.Smtp;
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

        public Task<Email_Services> Send_Email(Email_Services email)
        {
            var host = _configuration["SMTP:Host"];
            var port = int.Parse(_configuration["SMTP:Port"]);
            var username = _configuration["SMTP:Username"];
            var password = _configuration["SMTP:Password"];
        }
    }
}
