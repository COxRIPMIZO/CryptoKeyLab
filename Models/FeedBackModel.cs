using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class FeedBackModel
    {
        [Required]
        public string? Name { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Message { get; set; }
    }
}
