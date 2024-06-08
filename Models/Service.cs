using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class Service
    {
        [Key]
        [DisplayName("Id")]
        public int Service_Id { get; set; }
        [Required]
        [DisplayName("User Id")]
        public string User_Id { get; set; }
        [ForeignKey("User_Id")]
        public IdentityUser IdentityUser { get; set; }
        [Required]
        [DisplayName("Do Not Disturb")]
        public bool Do_Not_Disturb { get; set; }
        [Required]
        [DisplayName("Caller Tune Id")]
        public int Caller_Tune_Id { get; set; }
        [ForeignKey("Caller_Tune_Id")]
        public CallerTune Caller_Tune { get; set; }
    }
}
