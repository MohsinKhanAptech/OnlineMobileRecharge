using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Package
    {
        [Key]
        [DisplayName("Package Id")]
        public int Package_Id { get; set; }
        [Required]
        [DisplayName("Package Name")]
        public string Package_Name { get; set; }
        [Required]
        [DisplayName("Package Description")]
        public string Package_Description { get; set; }
        [Required]
        [DisplayName("Offnet Mins")]
        [RegularExpression("[0-9]+")]
        public Int64 Package_Off_Net_Mins { get; set; }
        [Required]
        [DisplayName("Onnet Mins")]
        [RegularExpression("[0-9]+")]
        public Int64 Package_On_Net_Mins { get; set; }
        [Required]
        [DisplayName("Internet Data")]
        [RegularExpression("[0-9]+")]
        public Int64 Package_Data { get; set; }
        [Required]
        [DisplayName("SMS")]
        [RegularExpression("[0-9]+")]
        public Int64 Package_SMS { get; set; }
        [Required]
        [DisplayName("Package Duration")]
        [RegularExpression("[0-9]+")]
        public int Package_Duration { get; set; }
        [Required]
        [DisplayName("Package Price")]
        [DataType(DataType.Currency)]
        public double Package_Price { get; set; }
        [Required]
        [EnumDataType(typeof(EnumPackageType))]
        [DisplayName("Package Type")]
        public EnumPackageType Package_Type { get; set; }
    }
    public enum EnumPackageType
    {
        Prepaid,
        Postpaid,
        Special
    }
}
