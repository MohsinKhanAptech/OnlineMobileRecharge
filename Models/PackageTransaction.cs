﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class PackageTransaction
    {
        [Key]
        [DisplayName("Id")]
        public int PackageTransaction_Id { get; set; }
        [Required]
        [DisplayName("User Id")]
        public required string User_Id { get; set; }
        [ForeignKey("User_Id")]
        public required IdentityUser IdentityUser { get; set; }
        [Required]
        [DisplayName("Package Id")]
        public int Package_Id { get; set; }
        [ForeignKey("Package_Id")]
        public required Package Package { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime Transaction_Date { get; }

        public PackageTransaction()
        {
            Transaction_Date = DateTime.Now;
        }
    }
}
