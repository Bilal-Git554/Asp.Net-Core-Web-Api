using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Library_Management.Entities
{
    public class Admin_Credentials
    {
        [EmailAddress]
        [Required]
        public string Admin_Email { get; set; }

        [Required]
        public string Admin_Password { get; set; }

    }
}
