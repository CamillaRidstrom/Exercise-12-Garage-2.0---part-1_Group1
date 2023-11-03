using System.ComponentModel.DataAnnotations;

namespace Exercise_12_Garage_2._0___part_1_Group1.Models
{
    public class ParkVehicle
    {
        [Key] public int Id { get; set; }
        public string RegistrationNumber { get; set; }

        public DateTime ParkingDate { get; set; }

        public VehicleType VehicleType { get; set; }

        public string Color { get; set;} = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int NumberOfWheels { get; set; }


    }
}
