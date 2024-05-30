using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Contact
    {
        [Key]
        [DisplayName("Id")]
        public int Contact_Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public required string Contact_Name { get; set; }
        [Required, EmailAddress]
        [DisplayName("Email")]
        public required string Contact_Email { get; set; }
        [Required, Phone]
        [DisplayName("Mobile Number")]
        public required string Contact_Phone { get; set; }
        [Required]
        [DisplayName("Intrest")]
        public required string Contact_Intrest { get; set; }
        [Required]
        [DisplayName("Message")]
        public required string Contact_Message { get; set; }
        [Required]
        [DisplayName("Date Added")]
        public DateTime Date_Added { get; }

        public Contact()
        {
            Date_Added = DateTime.Now;
        }
    }
}
