using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Library_Management.Entities
{
    public class User_Credentials
    {
        [Key]
        [EmailAddress]
        [Required]
        public string User_Email {  get; set; }

        [Required]
        public string User_Password { get; set; }

        [NotNull]
        public string User_Role { get; set; } = "User";
    }
}
