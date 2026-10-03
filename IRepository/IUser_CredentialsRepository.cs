using Library_Management.Entities;

namespace Library_Management.IRepository
{
    public interface IUser_CredentialsRepository
    {
        Task<User_Credentials> GetUserbyEmail(User_Credentials F);
        Task<User_Credentials> Add_User(User_Credentials C);
        Task<Forgot_Password?> Forgot_User(Forgot_Password email);
        Task<Reset_Password?> Reset_User(Reset_Password reset);
    }
}
