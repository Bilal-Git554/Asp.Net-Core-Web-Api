using Library_Management.Entities;
using Library_Management.IRepository;

namespace Library_Management.Repository
{
    public class JWT_Service_Repository : IJWT_Service
    {
        private readonly IConfiguration _configuration;
        public JWT_Service_Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Create_Token(string email)
        {
            return "";
        }
    }
}
