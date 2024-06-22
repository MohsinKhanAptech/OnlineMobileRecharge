using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class CallerTune
    {
        [Key]
        [DisplayName("Caller Tune Id")]
        public int Tune_Id { get; set; }
        [Required]
        [DisplayName("Caller Tune Name")]
        public string Tune_Name { get; set; }
        [Required]
        [DisplayName("Caller Tune Description")]
        public string Tune_Description { get; set; }
        [Required]
        [NotMapped]
        [DisplayName("Caller Tune File")]
        public IFormFile Tune_File { get; set; }
        [Required]
        [DisplayName("Caller Tune Path")]
        public string Tune_Path { get; set; }
        [Required]
        [DisplayName("Caller Tune Price")]
        public double Tune_Price { get; set; }
    }
}
