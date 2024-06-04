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
        public required string User_Id { get; set; }
        [ForeignKey("User_Id")]
        public required IdentityUser IdentityUser { get; set; }
        [Required]
        [DisplayName("Do Not Disturb")]
        public bool Do_Not_Disturb { get; set; }
        [Required]
        [DisplayName("Caller Tune Id")]
        public int Caller_Tune_Id { get; set; }
        [ForeignKey("Caller_Tune_Id")]
        public required CallerTune Caller_Tune { get; set; }
    }
}
