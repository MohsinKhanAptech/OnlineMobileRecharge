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
        public string Contact_Name { get; set; }
        [Required, EmailAddress]
        [DisplayName("Email")]
        public string Contact_Email { get; set; }
        [Required, Phone]
        [DisplayName("Mobile Number")]
        public string Contact_Phone { get; set; }
        [Required]
        [DisplayName("Intrest")]
        public string Contact_Intrest { get; set; }
        [Required]
        [DisplayName("Message")]
        public string Contact_Message { get; set; }
        [Required]
        [DisplayName("Date Added")]
        public DateTime Date_Added { get; set; }
    }
}
