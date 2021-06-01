using System;
using System.ComponentModel.DataAnnotations;

namespace HeavenProperty.Models
{
    public class UpdateNameViewModel
    {
        public UpdateNameViewModel()
        {
        }
        [Required(ErrorMessage = "First Name is required!")]
        public String FirstName { set; get; }
        [Required(ErrorMessage = "Last Name is required!")]
        public String LastName { set; get; }
    }
}
