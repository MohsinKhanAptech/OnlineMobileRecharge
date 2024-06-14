using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Contact
    {
        [Key]
        [DisplayName("Contact Id")]
        public int Contact_Id { get; set; }
        [Required]
        [DisplayName("Contact Name")]
        public string Contact_Name { get; set; }
        [Required, EmailAddress]
        [DisplayName("Contact Email")]
        public string Contact_Email { get; set; }
        [Required, Phone]
        [DisplayName("Contact Mobile Number")]
        public string Contact_Phone { get; set; }
        [Required]
        [DisplayName("Contact Intrest")]
        public string Contact_Intrest { get; set; }
        [Required]
        [DisplayName("Contact Message")]
        public string Contact_Message { get; set; }
        [Required]
        [DisplayName("Contact Date Added")]
        public DateTime Date_Added { get; set; }
    }
}
