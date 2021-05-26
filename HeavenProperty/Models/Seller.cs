using System;
namespace HeavenProperty.Models
{
    public class Seller
    {
        public Seller()
        {
        }
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
    }
}
