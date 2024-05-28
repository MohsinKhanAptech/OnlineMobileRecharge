using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Package
    {
        [Key]
        public int Package_Id { get; set; }
        [Required]
        public string Package_Name { get; set; }
        [Required]
        public string Package_Description { get; set; }
        [Required]
        public int Package_Off_Net_Mins { get; set; }
        [Required]
        public int Package_On_Net_Mins { get; set; }
        [Required]
        public int Package_Data { get; set; }
        [Required]
        public int Package_Sms { get; set; }
        [Required]
        public PackageType Package_Type { get; set; }
    }
    public enum PackageType
    {
        Prepaid,
        Postpaid
    }
}
