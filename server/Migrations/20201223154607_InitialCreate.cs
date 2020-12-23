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
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
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
                columns: new[] { "Id", "BusinessOwnerId", "Capacity", "Closes", "CreatedAt", "Name", "Opens", "OwnerEmail", "TimeSlotLength", "Type", "UpdatedAt", "Zip" },
                values: new object[,]
                {
                    { 1, null, 50, "16.00", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cool", "10.00", "test@test.com", 50, "Supermarket", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3520 },
                    { 2, null, 40, "14.00", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shop", "09.00", "test@test.com", 20, "Museum", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3520 },
                    { 3, null, 30, "15.30", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1337", "08.30", "test@test.com", 10, "Kiosk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4720 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "CreatedAt", "Id", "Name", "Password", "UpdatedAt", "Zip" },
                values: new object[,]
                {
                    { "test@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Peter", "$2a$11$vYP9R79B60omJPiIEsY02.Uid0sUfRy1FicmAQ.9SwbjvYps05VQO", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3520 },
                    { "h@h.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Jens", "$2a$11$A6j3YG/yP85TBK9CS.zg6OPLZByNpeXnDtYd6mMA0p7gCADrStcGi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2300 },
                    { "mads@hotmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Mads", "$2a$11$te/fo6sZzH6V1Wj9DvHMauLpjnsYEYcIytJUQbc86RApzb02oQn1C", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2700 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "UserEmail", "BusinessId", "CompanyEmail", "CreatedAt", "Id", "UpdatedAt" },
                values: new object[,]
                {
                    { "h@h.com", 1, null, new DateTime(2020, 12, 23, 16, 46, 6, 427, DateTimeKind.Local).AddTicks(5409), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "mads@hotmail.com", 1, null, new DateTime(2020, 12, 23, 16, 46, 6, 427, DateTimeKind.Local).AddTicks(6860), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "BusinessName", "Capacity", "CreatedAt", "End", "Start", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Cool", 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 23, 20, 46, 6, 427, DateTimeKind.Local).AddTicks(1166), new DateTime(2020, 12, 23, 19, 46, 6, 423, DateTimeKind.Local).AddTicks(1333), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "Cool", 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 23, 21, 46, 6, 427, DateTimeKind.Local).AddTicks(1604), new DateTime(2020, 12, 23, 20, 46, 6, 427, DateTimeKind.Local).AddTicks(1596), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "Cool", 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 23, 22, 46, 6, 427, DateTimeKind.Local).AddTicks(1609), new DateTime(2020, 12, 23, 21, 46, 6, 427, DateTimeKind.Local).AddTicks(1607), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
