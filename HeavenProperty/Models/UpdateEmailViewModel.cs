using System;
using System.ComponentModel.DataAnnotations;

namespace HeavenProperty.Models
{
    public class UpdateEmailViewModel
    {
        public UpdateEmailViewModel()
        {
        }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w[-\w.+]*@([A-Za-z0-9]+\.)+[A-Za-z]{2,14}$", ErrorMessage = "Invalid email format, " +
    "please type in a valid email address, Example: user@example.com!")]
        public String Email { set; get; }
    }
}
