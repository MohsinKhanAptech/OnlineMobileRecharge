using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Contact
    {
        [Key]
        public int Contact_Id { get; set; }
        [Required]
        public required string Contact_Name { get; set; }
        [Required, EmailAddress]
        public required string Contact_Email { get; set; }
        [Required, Phone]
        public required string Contact_Phone { get; set; }
        [Required]
        public required string Contact_Intrest { get; set; }
        [Required]
        public required string Contact_Message { get; set; }
    }
}
