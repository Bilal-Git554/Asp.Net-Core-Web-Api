using Library_Management.Connection;
using Library_Management.Entities;
using Library_Management.IRepository;
using Microsoft.EntityFrameworkCore;
namespace Library_Management.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> Read_Data()
        {
            var Read = await _context.Books.Include(r => r.Category).ToListAsync();
            return Read;
        }//Display All The Data

        public async Task<Book?> Readby_Id(int id)
        {
            var Id = await _context.Books.Include(b => b.Category).
                           FirstOrDefaultAsync(i => i.Book_Id == id);
            if(Id == null)
            {
                return null;
            }
            return Id;
        }//Display Data By Id

        public async Task<Book?> Create_Data(Book B)
        {
            B.Category = null;
            var Insert = await _context.Books.AddAsync(B);
            await _context.SaveChangesAsync();
            return B;
        }//Create New Data

        public async Task<Book?> Update_Data(int id , Book U)
        {
            var Update = await _context.Books.FindAsync(id);
            if (Update == null)
            {
                return null;
            }
            Update.Book_Name = U.Book_Name;
            Update.Author_Name = U.Author_Name;
            Update.About_Book = U.About_Book;
            Update.Published_Date = U.Published_Date;
            Update.Category_Id = U.Category_Id;
            await _context.SaveChangesAsync();
            return Update;
        }//Update Data By Id

        public async Task<Book?> Delete_Data(int id)
        {
            var Delete = await _context.Books.FindAsync(id);
            if (Delete == null)
            {
                return null;
            }
            _context.Books.Remove(Delete);
            await _context.SaveChangesAsync();
            return Delete;
        }//Delete Data By Id
    }
}
