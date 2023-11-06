using System.ComponentModel.DataAnnotations;

namespace Exercise_12_Garage_2._0___part_1_Group1.Models
{
    public class ParkVehicle
    {
        [Key] public int Id { get; set; }
        public string RegistrationNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime ParkingDate { get; set; }

        public VehicleType VehicleType { get; set; }

        public string Color { get; set;} = string.Empty;

        [StringLength(15)]
        public string Brand { get; set; } = string.Empty;

        [StringLength(20)]
        public string Model { get; set; } = string.Empty;

        [Range(0,10, ErrorMessage ="Wheels value should be within 0 and 10")]
        public int NumberOfWheels { get; set; }


    }
}
