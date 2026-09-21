using Library_Management.Entities;
using Library_Management.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email)
            };

            var key = _configuration["Jwt:Key"];
            //Finding The Key

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));
            //Converting The Secret Key Into Byte

            var credentials = new SigningCredentials(
                securityKey,SecurityAlgorithms.HmacSha256);
            //Using A HMAC-SHA256 Algorithm For The Bytes

            var token = new JwtSecurityToken(
                claims: claims,signingCredentials: credentials);
            //Create A Jwt Token For These Claims Based On These Credentials

            var tokenHandler = new JwtSecurityTokenHandler();
            //Changing The Jwt Token Object To String 

            return tokenHandler.WriteToken(token);
        }
    }
}
