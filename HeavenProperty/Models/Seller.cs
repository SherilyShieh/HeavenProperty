using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HeavenProperty.Models
{
    public class Seller
    {
        public Seller()
        {
        }
        public int Id { set; get; }
        [Required(ErrorMessage = "First Name is required!")]
        public String FirstName { set; get; }
        [Required(ErrorMessage = "Last Name is required!")]
        public String LastName { set; get; }
        [Required(ErrorMessage = "Address is required!")]
        public String Address { set; get; }
        [Required(ErrorMessage = "Phone is required!")]
        [RegularExpression(@"^[0][2][1579]{1}\d{6,7}$", ErrorMessage = "Please enter a valid New Zealand phone number！")]
        public String Phone { set; get; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^\w[-\w.+]*@([A-Za-z0-9]+\.)+[A-Za-z]{2,14}$", ErrorMessage = "Invalid email format, " +
            "please type in a valid email address, Example: user@example.com!")]
        public String Email { set; get; }
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The length of the password is at least 6 and max length is 20！")]
        public String Password { set; get; }
        [Required(ErrorMessage = "Please confirm your password!")]
        [Compare("Password", ErrorMessage = "Two different passwords!")]
        [NotMapped]
        public String ConfirmPassword { set; get; }
        public String GetFullName()
        {
            return this.FirstName + " " + this.LastName;
        }
        public override string ToString()
        {
            return "User [id=" + this.Id + ", name=" + this.GetFullName() + "]";
        }
    }
}
