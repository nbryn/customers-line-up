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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                    Type = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    OpeningTime = table.Column<double>(nullable: false),
                    ClosingTime = table.Column<double>(nullable: false),
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
                    UserId = table.Column<int>(nullable: true)
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
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BusinessOwners",
                columns: new[] { "Id", "UserEmail" },
                values: new object[] { 1, "h@h.com" });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "BusinessOwnerId", "Capacity", "ClosingTime", "Name", "OpeningTime", "OwnerEmail", "Type", "Zip" },
                values: new object[,]
                {
                    { 1, null, 50, 16.0, "Cool", 10.0, "h@h.com", "Supermarket", 3520 },
                    { 2, null, 40, 14.0, "Shop", 9.0, "h@h.com", "Museum", 3520 },
                    { 3, null, 30, 15.300000000000001, "1337", 8.3000000000000007, "h@h.com", "Kiosk", 4720 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Zip" },
                values: new object[] { 1, "h@h.com", "Jens", "$2a$11$aP6dsg5W1pDJIEbP6re9rOxq56SJcs9x85WMyeNqHPuPNDz1P6jCq", "3520" });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "Capacity", "End", "Start" },
                values: new object[] { 1, 1, 50, new DateTime(2020, 12, 5, 16, 38, 20, 26, DateTimeKind.Local).AddTicks(5957), new DateTime(2020, 12, 5, 15, 38, 20, 24, DateTimeKind.Local).AddTicks(1374) });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "Capacity", "End", "Start" },
                values: new object[] { 2, 1, 40, new DateTime(2020, 12, 5, 17, 38, 20, 26, DateTimeKind.Local).AddTicks(6289), new DateTime(2020, 12, 5, 16, 38, 20, 26, DateTimeKind.Local).AddTicks(6283) });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "Capacity", "End", "Start" },
                values: new object[] { 3, 1, 30, new DateTime(2020, 12, 5, 18, 38, 20, 26, DateTimeKind.Local).AddTicks(6292), new DateTime(2020, 12, 5, 17, 38, 20, 26, DateTimeKind.Local).AddTicks(6291) });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "UserId" },
                values: new object[] { "h@h.com", 1, null });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "UserId" },
                values: new object[] { "h@h.com", 2, null });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "UserId" },
                values: new object[] { "h@h.com", 3, null });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TimeSlotId",
                table: "Bookings",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

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
