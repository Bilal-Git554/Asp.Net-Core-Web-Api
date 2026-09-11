using Library_Management.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management.IRepository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> Read_Data();
        Task<Book?> Readby_Id(int id);
        Task<Book?> Create_Data(Book B);
        Task<Book?> Update_Data(int id, Book U);
        Task<Book?> Delete_Data(int id);

    }
}
