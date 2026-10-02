using System.ComponentModel.DataAnnotations;

namespace Library_Management.Entities
{
    public class Forgot_Password
    {
        [Required]
        [Key]
        public string? Email { get; set; }

        public string? Reset_Token { get; set; }
        public DateTime? Reset_Token_Expiry { get; set; }
    }
}
