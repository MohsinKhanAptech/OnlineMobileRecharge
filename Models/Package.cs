using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Package
    {
        [Key]
        [DisplayName("Id")]
        public int Package_Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public required string Package_Name { get; set; }
        [Required]
        [DisplayName("Description")]
        public required string Package_Description { get; set; }
        [Required]
        [DisplayName("Offnet Mins")]
        [RegularExpression("[0-9]+")]
        public Int64 Package_Off_Net_Mins { get; set; }
        [Required]
        [DisplayName("Onnet Mins")]
        [RegularExpression("[0-9]+")]
        public Int64 Package_On_Net_Mins { get; set; }
        [Required]
        [DisplayName("Data")]
        [RegularExpression("[0-9]+")]
        public Int64 Package_Data { get; set; }
        [Required]
        [DisplayName("SMS")]
        [RegularExpression("[0-9]+")]
        public Int64 Package_SMS { get; set; }
        [Required]
        [DisplayName("Duration")]
        [RegularExpression("[0-9]+")]
        public int Package_Duration { get; set; }
        [Required]
        [DisplayName("Price")]
        [DataType(DataType.Currency)]
        public double Package_Price { get; set; }
        [Required]
        [EnumDataType(typeof(EnumPackageType))]
        [DisplayName("Type")]
        public EnumPackageType Package_Type { get; set; }
    }
    public enum EnumPackageType
    {
        Prepaid,
        Postpaid
    }
}
