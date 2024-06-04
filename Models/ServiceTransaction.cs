using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CodeFixes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class ServiceTransaction
    {
        [Key]
        [DisplayName("Id")]
        public int ServiceTransaction_Id { get; set; }
        [Required]
        [DisplayName("Recharge Id")]
        public int Tune_Id { get; set; }
        [ForeignKey("Tune_Id")]
        public required CallerTune CallerTune { get; set; }
        [Required]
        [DisplayName("User Id")]
        public required string User_Id { get; set; }
        [ForeignKey("User_Id")]
        public required IdentityUser IdentityUser { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime Transaction_Date { get; }

        public ServiceTransaction()
        {
            Transaction_Date = DateTime.Now;
        }
    }
}
