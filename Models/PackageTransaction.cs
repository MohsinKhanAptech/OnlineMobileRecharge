using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class PackageTransaction
    {
        [Key]
        [DisplayName("Transaction Id")]
        public int PackageTransaction_Id { get; set; }
        [DisplayName("Session Id")]
        public string Session_Id { get; set; }
        [Required]
        [DisplayName("User Id")]
        public string User_Id { get; set; }
        [ForeignKey("User_Id")]
        public IdentityUser IdentityUser { get; set; }
        [Required]
        [DisplayName("Package Id")]
        public int Package_Id { get; set; }
        [ForeignKey("Package_Id")]
        public Package Package { get; set; }
        [Required, Phone]
        [DisplayName("Mobile Number")]
        public string Mobile_Number { get; set; }
        [Required]
        [DisplayName("Payment Completed?")]
        public bool Payment_Completed { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime Transaction_Date { get; set; }
    }
}
