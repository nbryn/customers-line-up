using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CLup.Migrations
{
    public partial class BookingWithBusinessId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "Bookings",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumns: new[] { "UserEmail", "TimeSlotId" },
                keyValues: new object[] { "h@h.com", 1 },
                column: "BusinessId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumns: new[] { "UserEmail", "TimeSlotId" },
                keyValues: new object[] { "h@h.com", 2 },
                column: "BusinessId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumns: new[] { "UserEmail", "TimeSlotId" },
                keyValues: new object[] { "h@h.com", 3 },
                column: "BusinessId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeSlotLength",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeSlotLength",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeSlotLength",
                value: 10);

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 11, 16, 55, 3, 743, DateTimeKind.Local).AddTicks(7467), new DateTime(2020, 12, 11, 15, 55, 3, 741, DateTimeKind.Local).AddTicks(2828) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 11, 17, 55, 3, 743, DateTimeKind.Local).AddTicks(7795), new DateTime(2020, 12, 11, 16, 55, 3, 743, DateTimeKind.Local).AddTicks(7789) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 11, 18, 55, 3, 743, DateTimeKind.Local).AddTicks(7798), new DateTime(2020, 12, 11, 17, 55, 3, 743, DateTimeKind.Local).AddTicks(7797) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$c.y.o2s.KGYj5uv9cldeZu2FZ1axUY97f47Gkq5HALb1W4mu/O05O");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BusinessId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumns: new[] { "UserEmail", "TimeSlotId" },
                keyValues: new object[] { "h@h.com", 1 },
                column: "BusinessId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumns: new[] { "UserEmail", "TimeSlotId" },
                keyValues: new object[] { "h@h.com", 2 },
                column: "BusinessId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumns: new[] { "UserEmail", "TimeSlotId" },
                keyValues: new object[] { "h@h.com", 3 },
                column: "BusinessId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeSlotLength",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeSlotLength",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeSlotLength",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 10, 0, 3, 3, 642, DateTimeKind.Local).AddTicks(5598), new DateTime(2020, 12, 9, 23, 3, 3, 640, DateTimeKind.Local).AddTicks(365) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 10, 1, 3, 3, 642, DateTimeKind.Local).AddTicks(6013), new DateTime(2020, 12, 10, 0, 3, 3, 642, DateTimeKind.Local).AddTicks(6006) });

            migrationBuilder.UpdateData(
                table: "TimeSlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2020, 12, 10, 2, 3, 3, 642, DateTimeKind.Local).AddTicks(6016), new DateTime(2020, 12, 10, 1, 3, 3, 642, DateTimeKind.Local).AddTicks(6015) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$1HZOnMyQp02yz7NE4.usRuWIYIbq/FT/dG8kd0Y58wFGXadBq1HKq");
        }
    }
}
