using Library_Management.Report;

namespace Library_Management.IRepository
{
    public interface IStockRepository
    {
        Task<Whole_Report> Report();
     }
}
