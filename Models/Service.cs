using System.ComponentModel.DataAnnotations;

namespace OnlineMobileRecharge.Models
{
    public class Service
    {
        [Key]
        public int Service_Id { get; set; }
        [Required]
        public int MyProperty { get; set; }
    }
}
