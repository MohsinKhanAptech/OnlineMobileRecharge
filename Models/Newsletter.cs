using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Newsletter
    {
        [Key]
        [DisplayName("Newsletter Id")]
        public int Newsletter_Id { get; set; }
        [Required, EmailAddress]
        [DisplayName("Newsletter Email")]
        public string Newsletter_Email { get; set; }
        [Required]
        [DisplayName("Newsletter Date Added")]
        public DateTime Date_Added { get; set; }
    }
}
