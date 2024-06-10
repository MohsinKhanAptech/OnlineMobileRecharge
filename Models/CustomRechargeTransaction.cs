using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class CustomRechargeTransaction
    {
        [Key]
        [DisplayName("Id")]
        public int CustomRecharge_Id { get; set; }
        [Required]
        [DisplayName("Session Id")]
        public string Session_Id { get; set; }
        [Required]
        [DisplayName("Price")]
        public double Recharge_Price { get; set; }
        [Required]
        [DisplayName("Tax Rate")]
        public int Tax_Id { get; set; }
        public TaxRate TaxRate { get; set; }
        [Required]
        [DisplayName("Amount")]
        public double Recharge_Amount { get; set; }
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
