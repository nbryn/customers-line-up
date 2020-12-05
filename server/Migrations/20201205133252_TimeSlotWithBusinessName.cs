using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CLup.Migrations
{
    public partial class TimeSlotWithBusinessName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessName",
                table: "TimeSlots",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessId",
                table: "Bookings",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BusinessName", "End", "Start" },
                values: new object[] { "Cool", new DateTime(2020, 12, 5, 18, 32, 52, 288, DateTimeKind.Local).AddTicks(9608), new DateTime(2020, 12, 5, 17, 32, 52, 286, DateTimeKind.Local).AddTicks(5326) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BusinessName", "End", "Start" },
                values: new object[] { "Cool", new DateTime(2020, 12, 5, 19, 32, 52, 288, DateTimeKind.Local).AddTicks(9931), new DateTime(2020, 12, 5, 18, 32, 52, 288, DateTimeKind.Local).AddTicks(9926) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BusinessName", "End", "Start" },
                values: new object[] { "Cool", new DateTime(2020, 12, 5, 20, 32, 52, 288, DateTimeKind.Local).AddTicks(9934), new DateTime(2020, 12, 5, 19, 32, 52, 288, DateTimeKind.Local).AddTicks(9933) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$Re7Ux29ld8L.aiBoQW4U6eH4fhTQfgOmhk5W3ENx70mbNf2GPcgsm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessName",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 5, 16, 38, 20, 26, DateTimeKind.Local).AddTicks(5957), new DateTime(2020, 12, 5, 15, 38, 20, 24, DateTimeKind.Local).AddTicks(1374) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 5, 17, 38, 20, 26, DateTimeKind.Local).AddTicks(6289), new DateTime(2020, 12, 5, 16, 38, 20, 26, DateTimeKind.Local).AddTicks(6283) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 5, 18, 38, 20, 26, DateTimeKind.Local).AddTicks(6292), new DateTime(2020, 12, 5, 17, 38, 20, 26, DateTimeKind.Local).AddTicks(6291) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$aP6dsg5W1pDJIEbP6re9rOxq56SJcs9x85WMyeNqHPuPNDz1P6jCq");
        }
    }
}
