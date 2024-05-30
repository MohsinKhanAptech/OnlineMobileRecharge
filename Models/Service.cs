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
        [ForeignKey("IdentityUser")]
        public required string User_Id { get; set; }
        public required IdentityUser IdentityUser { get; set; }
        [Required]
        [DisplayName("Do Not Disturb")]
        public bool Do_Not_Disturb { get; set; }
        [Required]
        [DisplayName("Caller Tune Id")]
        [ForeignKey("Caller_Tune")]
        public int Caller_Tune_Id { get; set; }
        public required CallerTune Caller_Tune { get; set; }
    }
}
