using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exercise_12_Garage_2._0___part_1_Group1.Migrations
{
    /// <inheritdoc />
    public partial class addedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "ParkVehicle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ParkingDate",
                table: "ParkVehicle",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "ParkingDate",
                value: new DateTime(2023, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "ParkingDate",
                value: new DateTime(2023, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "ParkingDate",
                value: new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "ParkingDate",
                value: new DateTime(2023, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ParkVehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "ParkingDate",
                value: new DateTime(2023, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingDate",
                table: "ParkVehicle");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "ParkVehicle",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
