using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class TaxRate
    {
        [Key]
        [DisplayName("Id")]
        public int Tax_Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public required string Tax_Name { get; set; }
        [Required]
        [DisplayName("Description")]
        public required string Tax_Description { get; set; }
        [Required]
        [DisplayName("Tax Rate")]
        public double Tax_Rate { get; set; }
    }
}
