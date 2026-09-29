using System.ComponentModel.DataAnnotations;

namespace Library_Management.Entities
{
    public class Forgot_Password
    {
        [Required]
        public string? Email { get; set; }

        public string? Reset_Token { get; set; }
    }
}
