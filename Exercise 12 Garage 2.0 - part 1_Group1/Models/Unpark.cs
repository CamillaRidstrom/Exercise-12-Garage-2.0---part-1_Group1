using System.ComponentModel.DataAnnotations;

namespace Exercise_12_Garage_2._0___part_1_Group1.Models
{
    public class Unpark
    {
       


        public double TimeElapsedInHours(ParkVehicle vehicle)
        {
            if (vehicle != null)
            {
                return (DateTime.Now - vehicle.ParkingDate).TotalHours;
            }
            else
            {
                return 0; // Handle the case when the ParkVehicle is not provided
            }
        }
    }
}