using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Feedback
    {
        [Key]
        public int Feedback_Id { get; set; }
        [Required]
        public int MyProperty { get; set; }
    }
}
