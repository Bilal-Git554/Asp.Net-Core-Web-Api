using Library_Management.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Library_Management.Report
{
    public class Stock
    {
        public string Book_Genre { get; set; } = string.Empty;
        public int Total_Books_In_Category { get; set; }
    }
}
