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
                name: "Employees",
                columns: table => new
                {
                    BusinessId = table.Column<int>(nullable: false),
                    UserEmail = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CompanyEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => new { x.UserEmail, x.BusinessId });
                    table.ForeignKey(
                        name: "FK_Employees_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
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
                values: new object[] { 1, "test@test.com" });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "BusinessOwnerId", "Capacity", "Closes", "Name", "Opens", "OwnerEmail", "TimeSlotLength", "Type", "Zip" },
                values: new object[,]
                {
                    { 1, null, 50, "16.00", "Cool", "10.00", "test@test.com", 50, "Supermarket", 3520 },
                    { 2, null, 40, "14.00", "Shop", "09.00", "test@test.com", 20, "Museum", 3520 },
                    { 3, null, 30, "15.30", "1337", "08.30", "test@test.com", 10, "Kiosk", 4720 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "Id", "Name", "Password", "Zip" },
                values: new object[,]
                {
                    { "test@test.com", 1, "Peter", "$2a$11$6IrGtqz4hlA.WUruI40oFemQgvDYCqkJY2hA13MiA3VzjJ4Jh2rWK", 3520 },
                    { "h@h.com", 2, "Jens", "$2a$11$oyuipSd8ivJJFtkLJUO3aO3sNCg/W.lREsQKV3TZEKGvLveL8B5ZG", 2300 },
                    { "mads@hotmail.com", 3, "Mads", "$2a$11$ajaWGNX8Bq6qBxSCihX4o.YFwhfRYnqtNqGKZ/KAsqOTAJBQACqIW", 2700 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "UserEmail", "BusinessId", "CompanyEmail", "CreatedAt", "Id" },
                values: new object[,]
                {
                    { "h@h.com", 1, null, new DateTime(2020, 12, 21, 15, 36, 36, 10, DateTimeKind.Local).AddTicks(6818), 1 },
                    { "mads@hotmail.com", 1, null, new DateTime(2020, 12, 21, 15, 36, 36, 10, DateTimeKind.Local).AddTicks(7728), 2 }
                });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "BusinessName", "Capacity", "End", "Start" },
                values: new object[,]
                {
                    { 1, 1, "Cool", 50, new DateTime(2020, 12, 21, 19, 36, 36, 10, DateTimeKind.Local).AddTicks(4007), new DateTime(2020, 12, 21, 18, 36, 36, 7, DateTimeKind.Local).AddTicks(9091) },
                    { 2, 1, "Cool", 40, new DateTime(2020, 12, 21, 20, 36, 36, 10, DateTimeKind.Local).AddTicks(4362), new DateTime(2020, 12, 21, 19, 36, 36, 10, DateTimeKind.Local).AddTicks(4355) },
                    { 3, 1, "Cool", 30, new DateTime(2020, 12, 21, 21, 36, 36, 10, DateTimeKind.Local).AddTicks(4365), new DateTime(2020, 12, 21, 20, 36, 36, 10, DateTimeKind.Local).AddTicks(4364) }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "BusinessId" },
                values: new object[] { "test@test.com", 1, 1 });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "BusinessId" },
                values: new object[] { "test@test.com", 2, 1 });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "BusinessId" },
                values: new object[] { "test@test.com", 3, 1 });

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
                name: "IX_Employees_BusinessId",
                table: "Employees",
                column: "BusinessId");

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
                name: "Employees");

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
