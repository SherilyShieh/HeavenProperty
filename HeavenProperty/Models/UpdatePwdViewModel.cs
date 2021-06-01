using System;
using System.ComponentModel.DataAnnotations;

namespace HeavenProperty.Models
{
    public class UpdatePwdViewModel
    {
        public UpdatePwdViewModel()
        {
        }
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The length of the password is at least 6 and max length is 20！")]
        public String Password { set; get; }
        [Compare("Password", ErrorMessage = "Two different passwords!")]
        public String ConfirmPassword { set; get; }
    }
}
