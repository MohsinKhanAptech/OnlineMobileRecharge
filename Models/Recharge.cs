using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Recharge
    {
        [Key]
        public int Recharge_Id { get; set; }
        [Required]
        public required string Recharge_Name { get; set; }
        [Required]
        public required string Recharge_Description { get; set; }
        [Required]
        public double Recharge_Amount { get; set; }
        [Required]
        public double Recharge_Price { get; set; }
        [Required]
        public double Recharge_Discount { get; set; }
        [Required]
        public RechargeType Recharge_Type { get; set; }
    }

    public enum RechargeType
    {
        Top_up,
        Special
    }
}
