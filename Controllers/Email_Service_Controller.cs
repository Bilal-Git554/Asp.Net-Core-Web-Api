using Library_Management.Entities;
using Library_Management.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Email_Service_Controller : Controller
    {
        private readonly IEmail_Service_Repository _Email_Service;
        public Email_Service_Controller(IEmail_Service_Repository Email_Service)
        {
            _Email_Service = Email_Service;
        }

        [HttpPost("Test")]
        public async Task<ActionResult> Send_Email(Email_Services email)
        {
            await _Email_Service.Send_Email(email);
            return Ok("Mail Sended Successfully!📨");
        }
    }
}
