using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class CustomRechargeTransaction
    {
        [Key]
        [DisplayName("Id")]
        public int CRecharge_Id { get; set; }
        [Required]
        [DisplayName("Price")]
        public double CRecharge_Price { get; set; }
        [Required]
        [DisplayName("Tax Rate")]
        public int Tax_Id { get; set; }
        public TaxRate TaxRate { get; set; }
        [Required, Phone]
        [DisplayName("Mobile Number")]
        public string Mobile_Number { get; set; }
        [Required]
        [EnumDataType(typeof(EnumRechargeType))]
        [DisplayName("Type")]
        public EnumRechargeType Recharge_Type { get; set; }
        [Required]
        [DisplayName("Transaction Date")]
        public DateTime Transaction_Date { get; set; }
    }
}
