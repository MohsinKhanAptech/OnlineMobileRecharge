using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnlineMobileRecharge.Models
{
    public class RechargeTransaction
    {
        [Key]
        [DisplayName("Transaction Id")]
        public int RechargeTransaction_Id { get; set; }
        [DisplayName("Session Id")]
        public string Session_Id { get; set; }
        [DisplayName("User Id")]
        [AllowNull]
        public string? User_Id { get; set; }
        [AllowNull]
        public IdentityUser? IdentityUser { get; set; }
        [Required]
        [DisplayName("Recharge Id")]
        public int Recharge_Id { get; set; }
        [ForeignKey("Recharge_Id")]
        public Recharge Recharge { get; set; }
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
