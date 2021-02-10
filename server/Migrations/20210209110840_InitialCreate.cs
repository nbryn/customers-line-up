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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false)
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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    OwnerEmail = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Opens = table.Column<string>(nullable: false),
                    Closes = table.Column<string>(nullable: false),
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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false),
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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
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
                columns: new[] { "Id", "CreatedAt", "UpdatedAt", "UserEmail" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "test@test.com" });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "Address", "BusinessOwnerId", "Capacity", "Closes", "CreatedAt", "Latitude", "Longitude", "Name", "Opens", "OwnerEmail", "TimeSlotLength", "Type", "UpdatedAt", "Zip" },
                values: new object[,]
                {
                    { 1, "Ryttergårdsvej 10", null, 50, "16.00", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 55.813741899999997, 55.813741899999997, "Cool", "10.00", "test@test.com", 50, "Supermarket", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { 2, "Farum Hovedgade 100", null, 40, "14.00", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12.3544073, 55.809126999999997, "Shop", "09.00", "test@test.com", 20, "Museum", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { 3, "Vermlandsgade 30", null, 30, "15.30", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12.598883300000001, 55.668441999999999, "1337", "08.30", "test@test.com", 10, "Kiosk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2300 - København S" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "Address", "CreatedAt", "Id", "Latitude", "Longitude", "Name", "Password", "UpdatedAt", "Zip" },
                values: new object[,]
                {
                    { "test@test.com", "Farum Hovedgade 15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12.370676, 55.812254000000003, "Peter", "$2a$11$CT568AIZOOFDsFf8xyYjFezlirZHT4tlrx8Nz0y4UthO67igTkEpW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { "h@h.com", "Farum Hovedgade 50", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 12.3640744, 55.810706000000003, "Jens", "$2a$11$9es7wNR3EhqITImSbBycne6vUToau2qi.fhlVbUy/8hWFKSZuKStu", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { "mads@hotmail.com", "Gedevasevej 15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 12.346788800000001, 55.807591500000001, "Mads", "$2a$11$GET1TIXTLsvNuWgAclOoxObHPFDIfFDCScfjKDIcv6I.fGnQ44kne", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { "emil@live.com", "Farum Hovedgade 15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 12.359132499999999, 55.820034200000002, "Emil", "$2a$11$Y/OoSr329mRKCnb8fzp6h.OG6HHS7t2T2AvjNI3D20DoBdtnOkcuG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "UserEmail", "BusinessId", "CompanyEmail", "CreatedAt", "Id", "UpdatedAt" },
                values: new object[,]
                {
                    { "h@h.com", 1, null, new DateTime(2021, 2, 9, 12, 8, 38, 938, DateTimeKind.Local).AddTicks(9471), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "mads@hotmail.com", 1, null, new DateTime(2021, 2, 9, 12, 8, 38, 939, DateTimeKind.Local).AddTicks(497), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "BusinessName", "Capacity", "CreatedAt", "End", "Start", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Cool", 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 9, 16, 8, 38, 938, DateTimeKind.Local).AddTicks(6027), new DateTime(2021, 2, 9, 15, 8, 38, 936, DateTimeKind.Local).AddTicks(9315), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "Cool", 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 9, 17, 8, 38, 938, DateTimeKind.Local).AddTicks(6451), new DateTime(2021, 2, 9, 16, 8, 38, 938, DateTimeKind.Local).AddTicks(6442), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "Cool", 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 9, 18, 8, 38, 938, DateTimeKind.Local).AddTicks(6455), new DateTime(2021, 2, 9, 17, 8, 38, 938, DateTimeKind.Local).AddTicks(6454), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "BusinessId", "CreatedAt", "UpdatedAt" },
                values: new object[] { "test@test.com", 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "BusinessId", "CreatedAt", "UpdatedAt" },
                values: new object[] { "test@test.com", 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "UserEmail", "TimeSlotId", "BusinessId", "CreatedAt", "UpdatedAt" },
                values: new object[] { "test@test.com", 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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
