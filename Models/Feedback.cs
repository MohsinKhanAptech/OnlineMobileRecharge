using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Feedback
    {
        [Key]
        [DisplayName("Id")]
        public int Feedback_Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public required string Feedback_Name { get; set; }
        [Required, EmailAddress]
        [DisplayName("Email")]
        public required string Feedback_Email { get; set; }
        [Required]
        [DisplayName("Message")]
        public required string Feedback_Message { get; set; }
        [Required]
        [DisplayName("Date Added")]
        public DateTime Date_Added { get; }

        public Feedback()
        {
            Date_Added = DateTime.Now;
        }
    }
}
