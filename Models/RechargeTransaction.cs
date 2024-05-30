using Microsoft.CodeAnalysis.CodeFixes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class RechargeTransaction
    {
        [Key]
        [DisplayName("Id")]
        public int RechargeTransaction_Id { get; set; }
        [Required]
        [DisplayName("Recharge Id")]
        [ForeignKey("Recharge")]
        public int Recharge_Id { get; set; }
        public required Recharge Recharge { get; set; }
        [Required, Phone]
        [DisplayName("Mobile Number")]
        public required string Mobile_Number { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime Transaction_Date { get; }

        public RechargeTransaction()
        {
            Transaction_Date = DateTime.Now;
        }
    }
}
