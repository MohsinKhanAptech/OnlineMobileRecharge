using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineMobileRecharge.Models
{
    public class CustomRechargeTransaction
    {
        [Key]
        [DisplayName("Transaction Id")]
        public int CustomRecharge_Id { get; set; }
        [Required]
        [DisplayName("Session Id")]
        public string Session_Id { get; set; }
        [DisplayName("User Id")]
        [AllowNull]
        public string? User_Id { get; set; }
        [AllowNull]
        public IdentityUser? IdentityUser { get; set; }
        [Required]
        [DisplayName("Recharge Price")]
        public double Recharge_Price { get; set; }
        [Required]
        [DisplayName("Tax Rate")]
        public int Tax_Id { get; set; }
        public TaxRate TaxRate { get; set; }
        [Required]
        [DisplayName("Recharge Amount")]
        public double Recharge_Amount { get; set; }
        [Required, Phone]
        [DisplayName("Mobile Number")]
        public string Mobile_Number { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime Transaction_Date { get; set; }
    }
}
