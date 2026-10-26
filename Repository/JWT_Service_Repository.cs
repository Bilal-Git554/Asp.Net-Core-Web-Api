using Library_Management.Entities;
using Library_Management.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library_Management.Repository
{
    public class JWT_Service_Repository : IJWT_ServiceRepository
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
            //Using A HMAC-SHA256 Algorithm On The Bytes For Signing The Token

            var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        signingCredentials: credentials);
            //Create A Jwt Token For These Claims Based On These Credentials

            var tokenHandler = new JwtSecurityTokenHandler();
            //Changing The Jwt Token Object To String 

            return tokenHandler.WriteToken(token);
        }
    }
}
