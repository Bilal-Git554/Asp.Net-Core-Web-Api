using Library_Management.Entities;

namespace Library_Management.IRepository
{
    public interface IUser_Credentials
    {
        Task<User_Credentials> GetUserbyEmail(User_Credentials F);
        Task<User_Credentials> Add_User(User_Credentials C);
    }
}
