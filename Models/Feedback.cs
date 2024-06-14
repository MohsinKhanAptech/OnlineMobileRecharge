using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Feedback
    {
        [Key]
        [DisplayName("Feedback Id")]
        public int Feedback_Id { get; set; }
        [Required]
        [DisplayName("Feedback Name")]
        public string Feedback_Name { get; set; }
        [Required, EmailAddress]
        [DisplayName("Feedback Email")]
        public string Feedback_Email { get; set; }
        [Required]
        [DisplayName("Feedback Message")]
        public string Feedback_Message { get; set; }
        [Required]
        [DisplayName("Feedback Date Added")]
        public DateTime Date_Added { get; set; }
    }
}
