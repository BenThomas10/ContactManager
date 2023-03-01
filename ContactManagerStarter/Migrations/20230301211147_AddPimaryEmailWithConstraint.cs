using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactManagerStarter.Migrations
{
    /// <inheritdoc />
    public partial class AddPimaryEmailWithConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAddresses_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "DOB", "FirstName", "LastName", "Title" },
                values: new object[,]
                {
                    { new Guid("930d4f10-9daf-4582-b4bb-cb9abfd382b3"), new DateTime(1960, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bill", "Gates", "Mr." },
                    { new Guid("99580d68-9d2f-4552-862e-06b3204193f1"), new DateTime(1980, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sundar", "Pichai", "Mr." },
                    { new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"), new DateTime(1950, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Steve", "Jobs", "Mr." }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "ContactId", "State", "Street1", "Street2", "Type", "Zip" },
                values: new object[,]
                {
                    { new Guid("1632295e-fc3c-49c5-b30f-d673fc816b73"), "Westbury", new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"), "NY", "245 Coral Place", "Appt #3", 0, 11590 },
                    { new Guid("7931505c-fca8-4a35-8d24-b32df341643d"), "Melvile", new Guid("99580d68-9d2f-4552-862e-06b3204193f1"), "NY", "10 Main St", "", 0, 11757 },
                    { new Guid("a0762d8b-9dc6-4fa0-99dc-6f7438732760"), "Los Angles", new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"), "CA", "1 Apple Way", "", 1, 11757 },
                    { new Guid("b39467d9-a1f4-4843-aee9-7e9060448ded"), "Melvile", new Guid("930d4f10-9daf-4582-b4bb-cb9abfd382b3"), "NY", "10 Main St", "", 0, 11757 }
                });

            migrationBuilder.InsertData(
                table: "EmailAddresses",
                columns: new[] { "Id", "ContactId", "Email", "IsPrimary", "Type" },
                values: new object[,]
                {
                    { new Guid("3a406f64-ad7b-4098-ab01-7e93aae2b851"), new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"), "SteveJobs@apple.com", false, 1 },
                    { new Guid("3ddeb084-5e5d-4eca-b275-e4f6886e04dc"), new Guid("b728f6ef-65d8-4da2-8e5f-0f67e3c3401c"), "Steve@Jobs.com", true, 0 },
                    { new Guid("5111f412-a7f4-4169-bb27-632687569ccd"), new Guid("930d4f10-9daf-4582-b4bb-cb9abfd382b3"), "Bill@gates.com", true, 0 },
                    { new Guid("d1a50413-20c0-4972-a351-8be24e1fc939"), new Guid("99580d68-9d2f-4552-862e-06b3204193f1"), "SundarPichai@gmail.com", true, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ContactId",
                table: "Addresses",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_ContactId_IsPrimary",
                table: "EmailAddresses",
                columns: new[] { "ContactId", "IsPrimary" },
                unique: true,
                filter: "[IsPrimary] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "EmailAddresses");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
