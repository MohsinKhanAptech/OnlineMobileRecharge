using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class TaxRate
    {
        [Key]
        [DisplayName("Tax Rate Id")]
        public int Tax_Id { get; set; }
        [Required]
        [DisplayName("Tax Rate Name")]
        public string Tax_Name { get; set; }
        [Required]
        [DisplayName("Tax Rate Description")]
        public string Tax_Description { get; set; }
        [Required]
        [DisplayName("Tax Rate")]
        public double Tax_Rate { get; set; }
    }
}
