﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class Package_Transaction
    {
        [Key]
        public int PackTran_Id { get; set; }
        [Required]
        [ForeignKey("IdentityUser")]
        public required string User_Id { get; set; }
        public required IdentityUser IdentityUser { get; set; }
        [Required]
        [ForeignKey("Package")]
        public int Package_Id { get; set; }
        public required Package Package { get; set; }
        [Required, Phone]
        public required string Mobile_Number { get; set; }
        [Required]
        public DateTime Transaction_Date { get; }

        public Package_Transaction()
        {
            Transaction_Date = DateTime.Now;
        }
    }
}
