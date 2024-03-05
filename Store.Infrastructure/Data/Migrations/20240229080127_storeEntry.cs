using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class storeEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b2f6c0bc-b9b9-4565-97b7-6422a5884832");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e1bd13ce-b845-4f06-987e-ef4d3765ece7");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "Invoices",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "Invoices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f9877700-e0df-4219-aeb7-6ec6e15c747f", null, "Member", "MEMBER" },
                    { "fbf3cfdf-6e65-4f7d-98ab-58ae025b5a88", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f9877700-e0df-4219-aeb7-6ec6e15c747f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fbf3cfdf-6e65-4f7d-98ab-58ae025b5a88");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "Invoices");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b2f6c0bc-b9b9-4565-97b7-6422a5884832", null, "Member", "MEMBER" },
                    { "e1bd13ce-b845-4f06-987e-ef4d3765ece7", null, "Admin", "ADMIN" }
                });
        }
    }
}
