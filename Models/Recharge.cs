using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class Recharge
    {
        [Key]
        [DisplayName("Recharge Id")]
        public int Recharge_Id { get; set; }
        [Required]
        [DisplayName("Recharge Name")]
        public string Recharge_Name { get; set; }
        [Required]
        [DisplayName("Recharge Description")]
        public string Recharge_Description { get; set; }
        [Required]
        [DisplayName("Recharge Amount")]
        [DataType(DataType.Currency)]
        public double Recharge_Amount { get; set; }
        [Required]
        [DisplayName("Recharge Price")]
        [DataType(DataType.Currency)]
        public double Recharge_Price { get; set; }
        [Required]
        [DisplayName("Tax Rate")]
        public double Recharge_Tax_Rate { get; set; }
        [Required]
        [DisplayName("Taxed Amount")]
        [DataType(DataType.Currency)]
        public double Recharge_Taxed_Amount { get; set; }
        [Required]
        [EnumDataType(typeof(EnumRechargeType))]
        [DisplayName("Recharge Type")]
        public EnumRechargeType Recharge_Type { get; set; }

        public Recharge()
        {
            Recharge_Taxed_Amount = Recharge_Price / Recharge_Tax_Rate * 100;
            Recharge_Amount = Recharge_Price - Recharge_Taxed_Amount;
        }
    }

    public enum EnumRechargeType
    {
        Top_up,
        Special
    }
}
