using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class Caller_Tune
    {
        [Key]
        public int Tune_Id { get; set; }
        [Required]
        public required string Tune_Name { get; set; }
        [Required]
        public required string Tune_Description { get; set; }
        [Required]
        [NotMapped]
        public required IFormFile Tume_File { get; set; }
        [FileExtensions(Extensions = ".mp3,.flac")]
        public required string Tune_Path { get; set; }
        [Required]
        public double Tune_Price { get; set; }
    }
}
