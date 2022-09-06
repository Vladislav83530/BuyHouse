using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyHouse.DAL.Entities.FlatEntity
{
    public class FlatParameters
    {
        [Key]
        public int Id { get; set; } 
        public string Type { get; set; }
        public string Description { get; set; }
        public int Rooms { get; set; }
        public string TypeOfWalls { get; set; }
        public double TotalArea { get; set; }   
        public double LivingArea { get; set; }
        public int Floor { get; set; }
        public string FeatureOfLayout { get; set; }
        public string Heating { get; set; }
        public int YearBuilt { get; set; }
        public string RegistrationNumber { get; set; }


        [ForeignKey("FlatID")]
        public int FlatID { get; set; }
        public Flat Flat { get; set; }
    }
}
