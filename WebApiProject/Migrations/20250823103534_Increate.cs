using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class Increate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CurrentMembers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GymMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Goals = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymMembers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GymMembers_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Capacity", "CurrentMembers", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 20, 0, "Yoga sessions for flexibility", "Yoga" },
                    { 2, 15, 0, "Strength training", "Weightlifting" },
                    { 3, 25, 0, "High-intensity cardio sessions", "Cardio" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role", "Token", "Username" },
                values: new object[,]
                {
                    { 1, "admin1@example.com", "admin123", "Admin", "", "admin1" },
                    { 2, "user1@example.com", "user123", "User", "", "user1" },
                    { 3, "user2@example.com", "user123", "User", "", "user2" }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "CategoryId", "Email", "Experience", "Name", "Phone", "Specialization" },
                values: new object[,]
                {
                    { 1, 1, "ramesh.kumar@example.com", 0, "Ramesh Kumar", null, "Yoga" },
                    { 2, 2, "anjali.singh@example.com", 0, "Anjali Singh", null, "Weightlifting" },
                    { 3, 3, "vikram.patel@example.com", 0, "Vikram Patel", null, "Cardio" }
                });

            migrationBuilder.InsertData(
                table: "GymMembers",
                columns: new[] { "Id", "CategoryId", "Email", "Goals", "JoinedDate", "Name", "Phone", "TrainerId" },
                values: new object[,]
                {
                    { 1, 3, "arjun.mehta@example.com", "Lose weight", new DateTime(2025, 7, 24, 10, 35, 32, 866, DateTimeKind.Utc).AddTicks(6341), "Arjun Mehta", "9876543210", 3 },
                    { 2, 1, "priya.sharma@example.com", "Increase flexibility", new DateTime(2025, 8, 13, 10, 35, 32, 866, DateTimeKind.Utc).AddTicks(6884), "Priya Sharma", "9876501234", 1 },
                    { 3, 2, "rohit.desai@example.com", "Build muscle", new DateTime(2025, 8, 18, 10, 35, 32, 866, DateTimeKind.Utc).AddTicks(6891), "Rohit Desai", "9876512345", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymMembers_CategoryId",
                table: "GymMembers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GymMembers_TrainerId",
                table: "GymMembers",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_CategoryId",
                table: "Trainers",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymMembers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
