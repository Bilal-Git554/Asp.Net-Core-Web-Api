using BCrypt.Net;
using Library_Management.Connection;
using Library_Management.Entities;
using Library_Management.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Library_Management.Repository
{
    public class User_Credentials_Repository : IUser_Credentials
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
    }
}
