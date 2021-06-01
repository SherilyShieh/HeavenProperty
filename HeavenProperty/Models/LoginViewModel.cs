using System;
using System.ComponentModel.DataAnnotations;

namespace HeavenProperty.Models
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
        }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w[-\w.+]*@([A-Za-z0-9]+\.)+[A-Za-z]{2,14}$", ErrorMessage = "Invalid email format, " +
         "please type in a valid email address, Example: user@example.com!")]
        public String Email { set; get; }
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The length of the password is at least 6 and max length is 20！")]
        public String Password { set; get; }
    }
}
