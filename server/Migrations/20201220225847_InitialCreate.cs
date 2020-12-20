using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CLup.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessOwners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Zip = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OwnerEmail = table.Column<string>(nullable: false),
                    Zip = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Opens = table.Column<string>(nullable: true),
                    Closes = table.Column<string>(nullable: true),
                    TimeSlotLength = table.Column<int>(nullable: false),
                    BusinessOwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Businesses_BusinessOwners_BusinessOwnerId",
                        column: x => x.BusinessOwnerId,
                        principalTable: "BusinessOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessId = table.Column<int>(nullable: false),
                    BusinessName = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    UserEmail = table.Column<string>(nullable: false),
                    TimeSlotId = table.Column<int>(nullable: false),
                    BusinessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => new { x.UserEmail, x.TimeSlotId });
                    table.ForeignKey(
                        name: "FK_Bookings_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BusinessOwners",
                columns: new[] { "Id", "UserEmail" },
                values: new object[] { 1, "h@h.com" });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "BusinessOwnerId", "Capacity", "Closes", "Name", "Opens", "OwnerEmail", "TimeSlotLength", "Type", "Zip" },
                values: new object[,]
                {
                    { 1, null, 50, "16.00", "Cool", "10.00", "h@h.com", 50, "Supermarket", 3520 },
                    { 2, null, 40, "14.00", "Shop", "09.00", "h@h.com", 20, "Museum", 3520 },
                    { 3, null, 30, "15.30", "1337", "08.30", "h@h.com", 10, "Kiosk", 4720 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "Id", "Name", "Password", "Zip" },
                values: new object[] { "h@h.com", 1, "Jens", "$2a$11$69zw5P1IKdOTGFWpMidtuukPhIpIuAGop4w9KIomdA7/f2NVJQwR2", 3520 });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "BusinessName", "Capacity", "End", "Start" },
                values: new object[] { 1, 1, "Cool", 50, new DateTime(2020, 12, 21, 3, 58, 46, 766, DateTimeKind.Local).AddTicks(5581), new DateTime(2020, 12, 21, 2, 58, 46, 764, DateTimeKind.Local).AddTicks(805) });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "BusinessName", "Capacity", "End", "Start" },
                values: new object[] { 2, 1, "Cool", 40, new DateTime(2020, 12, 21, 4, 58, 46, 766, DateTimeKind.Local).AddTicks(5933), new DateTime(2020, 12, 21, 3, 58, 46, 766, DateTimeKind.Local).AddTicks(5926) });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "BusinessName", "Capacity", "End", "Start" },
                values: new object[] { 3, 1, "Cool", 30, new DateTime(2020, 12, 21, 5, 58, 46, 766, DateTimeKind.Local).AddTicks(5936), new DateTime(2020, 12, 21, 4, 58, 46, 766, DateTimeKind.Local).AddTicks(5935) });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "BusinessId" },
                values: new object[] { "h@h.com", 1, 1 });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "BusinessId" },
                values: new object[] { "h@h.com", 2, 1 });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "BusinessId" },
                values: new object[] { "h@h.com", 3, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TimeSlotId",
                table: "Bookings",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_BusinessOwnerId",
                table: "Businesses",
                column: "BusinessOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessOwners_UserEmail",
                table: "BusinessOwners",
                column: "UserEmail",
                unique: true,
                filter: "[UserEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_BusinessId",
                table: "TimeSlots",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Businesses");

            migrationBuilder.DropTable(
                name: "BusinessOwners");
        }
    }
}
