using Library_Management.Entities;
using Library_Management.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_Credentials_Controller : ControllerBase
    {
        private readonly IUser_CredentialsRepository _user_Credentials;
        private readonly IJWT_ServiceRepository _jwt_Service;
        public User_Credentials_Controller(IUser_CredentialsRepository user_Credentials,
            IJWT_ServiceRepository jwt_Service)
        {
            _user_Credentials = user_Credentials;
            _jwt_Service = jwt_Service;
        }

        //T -> Token Purpose
        //F -> Fetch Purpose
        //C -> Create Purpose
        [HttpPost]
        public async Task<ActionResult<User_Credentials>> Add_User(User_Credentials C)
        {
            var Add = await _user_Credentials.Add_User(C);
            return Ok(Add);
        }


        [HttpPost("login")]
        public async Task<ActionResult<JWT_Service>> Login(User_Credentials T)
        {
            var user = await _user_Credentials.GetUserbyEmail(T);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = _jwt_Service.Create_Token(user.User_Email);

            return Ok(new JWT_Service
            {
                Email = user.User_Email,
                Token = token
            });
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<Forgot_Password>> Forgot_User(Forgot_Password email)
        {
            var forgot = await _user_Credentials.Forgot_User(email);
            if (forgot == null)
            {
                return NotFound();
            }
            return Ok(forgot);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<Reset_Password>> Reset_User(Reset_Password reset)
        {
            var reset_password = await _user_Credentials.Reset_User(reset);
            if (reset_password == null)
            {
                return NotFound("Invalid Or Expired Session!");
            }
            return Ok("Password Resetted Successfully!");
        }
    }
}
