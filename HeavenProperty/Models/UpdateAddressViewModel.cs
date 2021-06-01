using System;
using System.ComponentModel.DataAnnotations;

namespace HeavenProperty.Models
{
    public class UpdateAddressViewModel
    {
        public UpdateAddressViewModel()
        {
        }
        [Required(ErrorMessage = "Address is required!")]
        public String Address { set; get; }
    }
}
