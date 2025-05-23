using eUseControl.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Data.Entities.User
{
    public class UserTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Last Login")]
        public DateTime LastLogin { get; set; }

        [Display(Name = "Last Ip")]
        public string LastIp { get; set; }

        [Display(Name = "Level")]
        public URole Level { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}
