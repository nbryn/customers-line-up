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
                    Address = table.Column<string>(nullable: false)
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
                columns: new[] { "Id", "Address", "BusinessOwnerId", "Capacity", "Closes", "CreatedAt", "Name", "Opens", "OwnerEmail", "TimeSlotLength", "Type", "UpdatedAt", "Zip" },
                values: new object[,]
                {
                    { 1, "Farum Hovedgade 30", null, 50, "16.00", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cool", "10.00", "test@test.com", 50, "Supermarket", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { 2, "Farum Hovedgade 50", null, 40, "14.00", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shop", "09.00", "test@test.com", 20, "Museum", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { 3, "Vermlandsgade 30", null, 30, "15.30", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1337", "08.30", "test@test.com", 10, "Kiosk", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2300 - Købehavn S" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "Address", "CreatedAt", "Id", "Name", "Password", "UpdatedAt", "Zip" },
                values: new object[,]
                {
                    { "test@test.com", "Farum Hovedgade 10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Peter", "$2a$11$QRegsi/m4Qcrj/J9mFFe5eO1D1u0.0046156RwuTCFPZ5qcbWAnCW", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { "h@h.com", "Farum Hovedgade 15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Jens", "$2a$11$ZOohoS8BfVrTB/I/aHpgC./kzFBBVJ3FhUwq/4LRMAeqESFTkd4yq", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { "mads@hotmail.com", "Farum Hovedgade 15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Mads", "$2a$11$EKaLo43d4ff70FRR5e6.CutOlesDbyhmz1TzTPXXPgi8IUcwfREmi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" },
                    { "emil@live.com", "Farum Hovedgade 15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Emil", "$2a$11$peI.wJykQ/693/Bv8McdFe7u/UioiPLkv2iEHb9CCBn6De3vaAnV.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3520 - Farum" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "UserEmail", "BusinessId", "CompanyEmail", "CreatedAt", "Id", "UpdatedAt" },
                values: new object[,]
                {
                    { "h@h.com", 1, null, new DateTime(2020, 12, 28, 13, 49, 13, 645, DateTimeKind.Local).AddTicks(5331), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "mads@hotmail.com", 1, null, new DateTime(2020, 12, 28, 13, 49, 13, 645, DateTimeKind.Local).AddTicks(6260), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TimeSlots",
                columns: new[] { "Id", "BusinessId", "BusinessName", "Capacity", "CreatedAt", "End", "Start", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Cool", 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 28, 17, 49, 13, 645, DateTimeKind.Local).AddTicks(2448), new DateTime(2020, 12, 28, 16, 49, 13, 642, DateTimeKind.Local).AddTicks(4298), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "Cool", 40, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 28, 18, 49, 13, 645, DateTimeKind.Local).AddTicks(2803), new DateTime(2020, 12, 28, 17, 49, 13, 645, DateTimeKind.Local).AddTicks(2798), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "Cool", 30, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 28, 19, 49, 13, 645, DateTimeKind.Local).AddTicks(2807), new DateTime(2020, 12, 28, 18, 49, 13, 645, DateTimeKind.Local).AddTicks(2806), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
