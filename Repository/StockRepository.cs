using Library_Management.Connection;
using Library_Management.IRepository;
using Library_Management.Report;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Whole_Report> Report()
        {
            var Total_Stock = await _context.Books.CountAsync();
            var Read_ = await _context.Books.GroupBy(g => g.Category.Category_Name)
                .Select(g => new Stock
                {
                    Book_Genre = g.Key,
                    Total_Books_In_Category = g.Count(),
                }).ToListAsync();

            return new Whole_Report
            {
                Total_Books_Available = Total_Stock,
                Read_Stock = Read_
            };
        }
    }
}
