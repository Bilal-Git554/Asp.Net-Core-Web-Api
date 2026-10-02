using Library_Management.Entities;

namespace Library_Management.IRepository
{
    public interface IJWT_ServiceRepository
    {
        string Create_Token(string email);
    }
}
