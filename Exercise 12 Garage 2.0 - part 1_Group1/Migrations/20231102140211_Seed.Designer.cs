﻿// <auto-generated />
using Exercise_12_Garage_2._0___part_1_Group1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Exercise_12_Garage_2._0___part_1_Group1.Migrations
{
    [DbContext(typeof(Exercise_12_Garage_2_0___part_1_Group1Context))]
    [Migration("20231102140211_Seed")]
    partial class Seed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Exercise_12_Garage_2._0___part_1_Group1.Models.ParkVehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfWheels")
                        .HasColumnType("int");

                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ParkVehicle");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Boeing",
                            Color = "White",
                            Model = "A124",
                            NumberOfWheels = 6,
                            RegistrationNumber = "ABC 123",
                            VehicleType = 0
                        },
                        new
                        {
                            Id = 2,
                            Brand = "MasterCraft",
                            Color = "Yellow",
                            Model = "011",
                            NumberOfWheels = 0,
                            RegistrationNumber = "DEF 456",
                            VehicleType = 1
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Volvo",
                            Color = "Red",
                            Model = "Scania",
                            NumberOfWheels = 4,
                            RegistrationNumber = "GHI 789",
                            VehicleType = 2
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Toyota",
                            Color = "Black",
                            Model = "Corolla",
                            NumberOfWheels = 4,
                            RegistrationNumber = "JKL 012",
                            VehicleType = 3
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Cresent",
                            Color = "Blue",
                            Model = "92323",
                            NumberOfWheels = 2,
                            RegistrationNumber = "MNO 111",
                            VehicleType = 4
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
