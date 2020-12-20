using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CLup.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 21, 4, 16, 57, 582, DateTimeKind.Local).AddTicks(2778), new DateTime(2020, 12, 21, 3, 16, 57, 579, DateTimeKind.Local).AddTicks(5364) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 21, 5, 16, 57, 582, DateTimeKind.Local).AddTicks(3128), new DateTime(2020, 12, 21, 4, 16, 57, 582, DateTimeKind.Local).AddTicks(3122) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 21, 6, 16, 57, 582, DateTimeKind.Local).AddTicks(3178), new DateTime(2020, 12, 21, 5, 16, 57, 582, DateTimeKind.Local).AddTicks(3177) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "h@h.com",
                column: "Password",
                value: "$2a$11$8RsSUEKNfLG9jZSEyWniueh3AtZ1VtIMHVt3jZOBqLGt9wi.XPm4e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 21, 3, 58, 46, 766, DateTimeKind.Local).AddTicks(5581), new DateTime(2020, 12, 21, 2, 58, 46, 764, DateTimeKind.Local).AddTicks(805) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 21, 4, 58, 46, 766, DateTimeKind.Local).AddTicks(5933), new DateTime(2020, 12, 21, 3, 58, 46, 766, DateTimeKind.Local).AddTicks(5926) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 21, 5, 58, 46, 766, DateTimeKind.Local).AddTicks(5936), new DateTime(2020, 12, 21, 4, 58, 46, 766, DateTimeKind.Local).AddTicks(5935) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "h@h.com",
                column: "Password",
                value: "$2a$11$69zw5P1IKdOTGFWpMidtuukPhIpIuAGop4w9KIomdA7/f2NVJQwR2");
        }
    }
}
