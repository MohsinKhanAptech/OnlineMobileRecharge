using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Feedback
    {
        [Key]
        public int Feedback_Id { get; set; }
        [Required]
        public required string Feedback_Name { get; set; }
        [Required, EmailAddress]
        public required string Feedback_Email { get; set; }
        [Required]
        public required string Feedback_Message { get; set; }
        [Required]
        public DateTime Date_Added { get; }

        public Feedback()
        {
            Date_Added = DateTime.Now;
        }
    }
}
