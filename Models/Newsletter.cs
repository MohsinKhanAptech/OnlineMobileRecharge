using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Newsletter
    {
        [Key]
        [DisplayName("Id")]
        public int Newsletter_Id { get; set; }
        [Required, EmailAddress]
        [DisplayName("Email")]
        public required string Newsletter_Email { get; set; }
        [Required]
        [DisplayName("Date Added")]
        public DateTime Date_Added { get; }

        public Newsletter()
        {
            Date_Added = DateTime.Now;
        }
    }
}
