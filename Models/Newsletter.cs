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
        [Required]
        public DateTime Date_Added { get; }

        public Newsletter()
        {
            Date_Added = DateTime.Now;
        }
    }
}
