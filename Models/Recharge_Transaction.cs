using Microsoft.CodeAnalysis.CodeFixes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class Recharge_Transaction
    {
        [Key]
        public int RechTran_Id { get; set; }
        [Required]
        [ForeignKey("Recharge")]
        public int Recharge_Id { get; set; }
        public required Recharge Recharge { get; set; }
        [Required, Phone]
        public required string Mobile_Number { get; set; }
        [Required]
        public DateTime Transaction_Date { get; }

        public Recharge_Transaction()
        {
            Transaction_Date = DateTime.Now;
        }
    }
}
