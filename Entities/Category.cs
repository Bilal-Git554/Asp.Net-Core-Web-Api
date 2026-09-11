using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library_Management.Entities
{
    public class Category
    {
        [Key]
        public int Category_Id { get; set; }

        [Required]
        public string ? Category_Name { get; set; }

        [JsonIgnore]
        public List<Book> ? Books { get; set; }
    }
}
