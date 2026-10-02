using Library_Management.Connection;
using Library_Management.Entities;
using Library_Management.IRepository;
using System.Security.Cryptography;


namespace Library_Management.Repository
{
    public class User_Credentials_Repository : IUser_CredentialsRepository
    {
        private ApplicationDbContext _context;
        public User_Credentials_Repository(ApplicationDbContext context)
        {
            _context = context;
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
            var forgot = await _context.User_Credentials.FindAsync(email.Email);
            if (forgot == null)
            {
                return null;
            }

            var TokenBytes = RandomNumberGenerator.GetBytes(32);
            var ResetToken = Convert.ToBase64String(TokenBytes);
            var ResetLink = $"http://localhost:4200/reset-password?token={ResetToken}";
            Console.WriteLine($"Reset Link: {ResetLink}");

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
}
