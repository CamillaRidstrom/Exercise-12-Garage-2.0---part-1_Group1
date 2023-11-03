using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercise_12_Garage_2._0___part_1_Group1.Models;

namespace Exercise_12_Garage_2._0___part_1_Group1.Data
{
    public class Exercise_12_Garage_2_0___part_1_Group1Context : DbContext
    {
        public Exercise_12_Garage_2_0___part_1_Group1Context (DbContextOptions<Exercise_12_Garage_2_0___part_1_Group1Context> options)
            : base(options)
        {
        }

        public DbSet<ParkVehicle> ParkVehicle => Set<ParkVehicle>();    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParkVehicle>().HasData(
                new ParkVehicle { Id = 1, RegistrationNumber = "ABC 123", ParkingDate = DateTime.Parse("2023-10-25"), VehicleType = VehicleType.Airplane, Color = "White", Brand = "Boeing",Model="A124",NumberOfWheels=6 },
                new ParkVehicle { Id = 2, RegistrationNumber = "DEF 456", ParkingDate = DateTime.Parse("2023-10-30"), VehicleType = VehicleType.Boat, Color = "Yellow", Brand = "MasterCraft", Model = "011", NumberOfWheels = 0 },
                new ParkVehicle { Id = 3, RegistrationNumber = "GHI 789", ParkingDate = DateTime.Parse("2023-11-01"), VehicleType = VehicleType.Bus, Color = "Red", Brand = "Volvo", Model = "Scania", NumberOfWheels = 4 },
                new ParkVehicle { Id = 4, RegistrationNumber = "JKL 012", ParkingDate = DateTime.Parse("2023-11-02"), VehicleType = VehicleType.Car, Color = "Black", Brand = "Toyota", Model = "Corolla", NumberOfWheels = 4},
                new ParkVehicle { Id = 5, RegistrationNumber = "MNO 111", ParkingDate = DateTime.Parse("2023-11-03"),VehicleType = VehicleType.Motorcycle, Color = "Blue", Brand = "Cresent", Model = "92323", NumberOfWheels = 2 }
                );
        }
    }
}
