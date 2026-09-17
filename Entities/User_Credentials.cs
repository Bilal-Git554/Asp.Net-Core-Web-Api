using System.ComponentModel.DataAnnotations;

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
    }
}
