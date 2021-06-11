using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoWeb.Data.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Username")]
        public string EmailId { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
    }
    public class LoginResponseModel {
        public string token { get; set; }
    }
}
