using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreCategoriesAndTrainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Capacity", "CurrentMembers", "Description", "Name" },
                values: new object[,]
                {
                    { 4, 18, 0, "High-intensity functional training", "CrossFit" },
                    { 5, 12, 0, "Core strength and posture improvement", "Pilates" },
                    { 6, 30, 0, "Dance-based cardio workout", "Zumba" }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "CategoryId", "Email", "Experience", "Name", "Phone", "Specialization" },
                values: new object[,]
                {
                    { 7, 2, "amit.sharma@example.com", 0, "Amit Sharma", null, "Strength & Conditioning" },
                    { 8, 1, "divya.kapoor@example.com", 0, "Divya Kapoor", null, "Advanced Yoga" },
                    { 4, 4, "sandeep.rao@example.com", 0, "Sandeep Rao", null, "CrossFit" },
                    { 5, 5, "meera.joshi@example.com", 0, "Meera Joshi", null, "Pilates" },
                    { 6, 6, "pooja.nair@example.com", 0, "Pooja Nair", null, "Zumba" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Trainers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
