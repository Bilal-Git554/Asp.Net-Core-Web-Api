using Library_Management.Entities;

namespace Library_Management.IRepository
{
    public interface IEmail_Service_Repository
    {
        Task Send_Email(Email_Services email);
    }
}
