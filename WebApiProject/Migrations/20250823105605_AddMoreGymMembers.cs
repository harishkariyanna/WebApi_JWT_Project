using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreGymMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GymMembers",
                columns: new[] { "Id", "CategoryId", "Email", "Goals", "JoinedDate", "Name", "Phone", "TrainerId" },
                values: new object[,]
                {
                    { 4, 3, "neha.verma@example.com", "Improve stamina", new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neha Verma", "9876523456", 3 },
                    { 5, 1, "karan.malhotra@example.com", "Reduce stress", new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Karan Malhotra", "9876534567", 1 },
                    { 6, 2, "sunita.nair@example.com", "Gain muscle", new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sunita Nair", "9876545678", 2 },
                    { 7, 3, "rajeev.kumar@example.com", "Maintain fitness", new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rajeev Kumar", "9876556789", 3 },
                    { 8, 1, "anita.shetty@example.com", "Lose belly fat", new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anita Shetty", "9876567890", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
