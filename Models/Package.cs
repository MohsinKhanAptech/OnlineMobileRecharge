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
        [Range(0,int.MaxValue)]
        public int Package_Off_Net_Mins { get; set; }
        [Required]
        [DisplayName("Onnet Mins")]
        [Range(0,int.MaxValue)]
        public int Package_On_Net_Mins { get; set; }
        [Required]
        [DisplayName("Data")]
        [Range(0,int.MaxValue)]
        public int Package_Data { get; set; }
        [Required]
        [DisplayName("SMS")]
        [Range(0,int.MaxValue)]
        public int Package_SMS { get; set; }
        [Required]
        [DisplayName("Duration")]
        [Range(0,int.MaxValue)]
        public int Package_Duration { get; set; }
        [Required]
        [DisplayName("Price")]
        public int Package_Price { get; set; }
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
