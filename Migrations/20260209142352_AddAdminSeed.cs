using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RoomStatus",
                table: "Rooms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RoomCode",
                table: "Rooms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StatusBooking",
                table: "BookingStatuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "Bookings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "BookingHistories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "BookingStatuses",
                columns: new[] { "Id", "StatusBooking" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Rejected" },
                    { 4, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "CreatedAt", "Location", "RoomCode", "RoomName", "RoomStatus", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 60, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gedung D4 Lantai 3", "A3", "Ruang Kuliah A 301", "Available", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 45, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gedung D4 Lantai 2", "B2", "Ruang Kuliah B 202", "Available", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gedung Pascasarjana Lantai 6", "Aula-Mini", "Mini Theater", "Available", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 30, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gedung D4 Lantai 1", "LAB-C3", "Lab Komputer Jaringan C 307", "Available", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 30, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gedung D4 Lantai 2", "LAB-C2", "Lab APD C 205", "Maintenance", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 500, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gedung Pascasarjana Lantai 6", "Aula-Utama", "AUDITORIUM", "Available", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 15, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gedung SAW Lantai 6", "SAW", "Ruang Kelas SAW-06.07", "Available", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 50, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gedung Pascasarjana Lantai 5", "PS", "Ruang Kelas PS-05.12", "Available", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookingStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookingStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookingStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookingStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "RoomStatus",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RoomCode",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "StatusBooking",
                table: "BookingStatuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "BookingHistories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
