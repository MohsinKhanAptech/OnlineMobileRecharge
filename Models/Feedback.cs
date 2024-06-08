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
        public string Feedback_Name { get; set; }
        [Required, EmailAddress]
        [DisplayName("Email")]
        public string Feedback_Email { get; set; }
        [Required]
        [DisplayName("Message")]
        public string Feedback_Message { get; set; }
        [Required]
        [DisplayName("Date Added")]
        public DateTime Date_Added { get; set; }
    }
}
