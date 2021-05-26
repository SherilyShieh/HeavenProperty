using System;
namespace HeavenProperty.Models
{
    public class Property
    {
        public Property()
        {
        }
        public int Id { set; get; }
        public string Title { set; get; }
        public string Location { set; get; }
        public int Rooms { set; get; }
        public int BathRoom { set; get; }
        public int CarParking { set; get; }
        public string Type { set; get; }
        public string FloorArea { set; get; }
        public string LandArea { set; get; }
        public string RV { set; get; }
        public string Email { set; get; }
        public string Icon { set; get; }
        public int Seller_Id { set; get; }
    }
}
