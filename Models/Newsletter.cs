using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Newsletter
    {
        [Key]
        public int Newsletter_Id { get; set; }
        [Required]
        [EmailAddress]
        public string Newsletter_Email { get; set; }
    }
}
