﻿using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CodeFixes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class ServiceTransaction
    {
        [Key]
        [DisplayName("Transaction Id")]
        public int ServiceTransaction_Id { get; set; }
        [DisplayName("Session Id")]
        public string Session_Id { get; set; }
        [Required]
        [DisplayName("Caller Tune Id")]
        public int Tune_Id { get; set; }
        [ForeignKey("Tune_Id")]
        public CallerTune CallerTune { get; set; }
        [Required]
        [DisplayName("User Id")]
        public string User_Id { get; set; }
        [ForeignKey("User_Id")]
        public IdentityUser IdentityUser { get; set; }
        [Required, Phone]
        [DisplayName("Mobile Number")]
        public string Mobile_Number { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime Transaction_Date { get; set; }
    }
}
