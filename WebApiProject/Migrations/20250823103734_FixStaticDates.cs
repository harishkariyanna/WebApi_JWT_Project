using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class FixStaticDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinedDate",
                value: new DateTime(2025, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 2,
                column: "JoinedDate",
                value: new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinedDate",
                value: new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 1,
                column: "JoinedDate",
                value: new DateTime(2025, 7, 24, 10, 35, 32, 866, DateTimeKind.Utc).AddTicks(6341));

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 2,
                column: "JoinedDate",
                value: new DateTime(2025, 8, 13, 10, 35, 32, 866, DateTimeKind.Utc).AddTicks(6884));

            migrationBuilder.UpdateData(
                table: "GymMembers",
                keyColumn: "Id",
                keyValue: 3,
                column: "JoinedDate",
                value: new DateTime(2025, 8, 18, 10, 35, 32, 866, DateTimeKind.Utc).AddTicks(6891));
        }
    }
}
