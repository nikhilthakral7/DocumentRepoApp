using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepoWeb.Data.Models
{
    public class RegisterModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string firstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string lastName { get; set; }

        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string phNum { get; set; }

        [Required]
        public string userEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string userPassword { get; set; }

        [Required]
        [Compare("userPassword", ErrorMessage ="Password do not match")]
        [Display(Name = "Confirm Password")]
        public string userConfirmPassword { get; set; }

        //Setting default value to 0 while creating new user
        public long CreatedBy { get; set; } = 0;

        //Setting default value to 0 while creating new user
        public long ModifieddBy { get; set; } = 0;

    }
}
