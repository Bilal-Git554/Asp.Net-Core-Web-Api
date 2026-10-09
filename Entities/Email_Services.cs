using System.ComponentModel.DataAnnotations;

namespace Library_Management.Entities
{
    public class Email_Services
    {
        [Required]
        [EmailAddress]
        public string? To { get; set; }

        [Required]
        public string? Subject { get; set; }

        [Required]
        public string? Body { get; set; }
    }
}
