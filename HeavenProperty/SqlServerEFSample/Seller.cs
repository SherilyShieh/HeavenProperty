using System;
using System.ComponentModel.DataAnnotations;

namespace SqlServerEFSample
{
    public class Seller
    {
        public Seller()
        {
        }
        public int Id { set; get; }
        [Required]
        public String FirstName { set; get; }
        [Required]
        public String LastName { set; get; }
        [Required]
        public String Address { set; get; }
        [Required]
        public String Phone { set; get; }
        [Required]
        public String Email { set; get; }
        [Required]
        public String Password { set; get; }
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
