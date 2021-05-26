using System;
using System.ComponentModel.DataAnnotations;

namespace SqlServerEFSample
{
    public class Property
    {
        public Property()
        {
        }
        public int Id { set; get; }
        [Required]
        public String Title { set; get; }
        [Required]
        public String Location { set; get; }
        [Required]
        public int Rooms { set; get; }
        [Required]
        public int BathRooms { set; get; }
        [Required]
        public int CarParkings { set; get; }
        [Required]
        public String Type { set; get; }
        [Required]
        public String FloorArea { set; get; }
        [Required]
        public String LandArea { set; get; }
        [Required]
        public String RV { set; get; }
        [Required]
        public String Email { set; get; }
        public String Icon { set; get; }
        [Required]
        public int Seller_Id { set; get; }
        public virtual Seller AssignedTo { get; set; }
    }
}
