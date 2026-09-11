using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Library_Management.Entities
{
    public class Book
    {
        [Key]
        public int Book_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Book_Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Author_Name { get; set; }

        [Required]
        [StringLength(100)]
        public string About_Book { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Published_Date { get; set; }

        [Required]
        [NotNull]
        public int Category_Id { get; set; }

        [ForeignKey("Category_Id")]
        public Category ? Category { get; set; }

    }
}
