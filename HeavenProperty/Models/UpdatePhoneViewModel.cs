using System;
using System.ComponentModel.DataAnnotations;

namespace HeavenProperty.Models
{
    public class UpdatePhoneViewModel
    {
        public UpdatePhoneViewModel()
        {
        }
        [Required(ErrorMessage = "Phone is required!")]
        [RegularExpression(@"^[0][2][1579]{1}\d{6,7}$", ErrorMessage = "Please enter a valid New Zealand phone number！")]
        public String Phone { set; get; }
    }
}
