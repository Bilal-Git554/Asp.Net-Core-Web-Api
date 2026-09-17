using Microsoft.EntityFrameworkCore;
using Library_Management.Entities;

namespace Library_Management.Connection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User_Credentials> User_Credentials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(c => c.Book_Id).ValueGeneratedNever();
            modelBuilder.Entity<Category>().Property(c => c.Category_Id).ValueGeneratedNever();
        }
    }  
}
