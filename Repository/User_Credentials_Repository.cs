using Library_Management.Connection;
using Library_Management.Entities;
using Library_Management.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;


namespace Library_Management.Repository
{
    public class User_Credentials_Repository : IUser_CredentialsRepository
    {
        private ApplicationDbContext _context;
        private readonly IEmail_Service_Repository _email_service;
        public User_Credentials_Repository(ApplicationDbContext context,
            IEmail_Service_Repository email_service)
        {
            _context = context;
            _email_service = email_service;
        }

        //F -> Fetch Purpose
        //C -> Create Purpose
        public async Task<User_Credentials> GetUserbyEmail(User_Credentials F)
        {
            var user = await _context.User_Credentials.FindAsync(F.User_Email);
            if (user == null)
            {
                return null;
            }
            
            var valid_Password = BCrypt.Net.BCrypt.EnhancedVerify(F.User_Password, user.User_Password);
            if(valid_Password)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<User_Credentials> Add_User(User_Credentials C)
        {
            C.User_Password = BCrypt.Net.BCrypt.EnhancedHashPassword(C.User_Password,workFactor : 13);
            await _context.AddAsync(C);
            await _context.SaveChangesAsync();
            return C;
        }

        public async Task<Forgot_Password?> Forgot_User(Forgot_Password email)
        {
            var already_exists = await _context.Forgot_Password.FindAsync(email.Email);
            if (already_exists != null)
            {
                var TokenBytes = RandomNumberGenerator.GetBytes(32);
                var ResetToken = Convert.ToBase64String(TokenBytes);
                var ResetLink = $"http://localhost:4200/reset-password?token={ResetToken}";

                var to_email_service = new Email_Service
                {
                    To = email.Email,
                    Subject = "Antheaneum Library - Reset Password",
                    Body = $"Click the following link to reset your password: {ResetLink}"
                };

                await _email_service.Send_Email(to_email_service);

                already_exists.Email = email.Email;
                already_exists.Reset_Token = ResetToken;
                already_exists.Reset_Token_Expiry = DateTime.UtcNow.AddMinutes(30);

                _context.Forgot_Password.Update(already_exists);
                await _context.SaveChangesAsync();

                return already_exists;
            }

            else
            {
                var new_addition = await _context.User_Credentials.FindAsync(email.Email);
                if (new_addition != null)
                {

                    var TokenBytes = RandomNumberGenerator.GetBytes(32);
                    var ResetToken = Convert.ToBase64String(TokenBytes);
                    var ResetLink = $"http://localhost:4200/reset-password?token={ResetToken}";

                    var to_email_service = new Email_Service
                    {
                        To = email.Email,
                        Subject = "Antheaneum Library - Reset Password",
                        Body = $"Click the following link to reset your password: {ResetLink}"
                    };

                    await _email_service.Send_Email(to_email_service);

                    var reset = new Forgot_Password
                    {
                        Email = email.Email,
                        Reset_Token = ResetToken,
                        Reset_Token_Expiry = DateTime.UtcNow.AddMinutes(30)
                    };

                    await _context.Forgot_Password.AddAsync(reset);
                    await _context.SaveChangesAsync();

                    return reset;
                }
            }

            return null;
        }


        public async Task<Reset_Password?> Reset_User(Reset_Password reset)
        {
            var find_token = await _context.Forgot_Password.FirstOrDefaultAsync(x => x.Reset_Token == reset.Token);
            if(find_token == null)
            {
                return null; 
            }
            if(find_token.Reset_Token_Expiry < DateTime.UtcNow)
            {
                return null;
            }
            else
            {
                var update_password = await _context.User_Credentials.FindAsync(find_token.Email);
                if (update_password == null)
                {
                    return null;
                }
                reset.New_Password = BCrypt.Net.BCrypt.EnhancedHashPassword(reset.New_Password, workFactor: 13);
                update_password.User_Password = reset.New_Password;

                _context.User_Credentials.Update(update_password);
                await _context.SaveChangesAsync();

                return reset;
            }
        }
    }
}
