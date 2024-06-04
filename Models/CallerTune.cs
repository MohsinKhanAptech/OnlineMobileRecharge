﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class CallerTune
    {
        [Key]
        [DisplayName("Id")]
        public int Tune_Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public required string Tune_Name { get; set; }
        [Required]
        [DisplayName("Description")]
        public required string Tune_Description { get; set; }
        [Required]
        [NotMapped]
        [DisplayName("File")]
        [FileExtensions(Extensions = ".mp3,.flac")]
        public IFormFile Tune_File { get; set; }
        [Required]
        [DisplayName("Path")]
        public required string Tune_Path { get; set; }
        [Required]
        [DisplayName("Price")]
        public double Tune_Price { get; set; }
    }
}
