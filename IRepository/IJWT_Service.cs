using Library_Management.Entities;

namespace Library_Management.IRepository
{
    public interface IJWT_Service
    {
        string Create_Token(string email);
    }
}
