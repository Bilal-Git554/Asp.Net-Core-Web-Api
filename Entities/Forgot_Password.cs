using System.ComponentModel.DataAnnotations;

namespace Library_Management.Entities
{
    public class Forgot_Password
    {
        [Required]
        public string Email { get; set; }
    }
}
