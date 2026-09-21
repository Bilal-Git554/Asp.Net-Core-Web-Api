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
        private IUser_Credentials _user_Credentials;
        private IJWT_Service _jwt_Service;
        public User_Credentials_Controller(IUser_Credentials user_Credentials,
            IJWT_Service jwt_Service)
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


        [HttpPost("{login}")]
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
    }
}
