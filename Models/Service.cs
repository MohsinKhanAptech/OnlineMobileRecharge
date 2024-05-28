using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMobileRecharge.Models
{
    public class Service
    {
        [Key]
        public int Service_Id { get; set; }
        [Required]
        [ForeignKey("IdentityUser")]
        public required string User_Id { get; set; }
        public required IdentityUser IdentityUser { get; set; }
        [Required]
        public bool Do_Not_Disturb { get; set; }
        [Required]
        [ForeignKey("Caller_Tune")]
        public int Caller_Tune_Id { get; set; }
        public required Caller_Tune Caller_Tune { get; set; }
    }
}
