using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnChange5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceSender_SenderId",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceSender",
                table: "InvoiceSender");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3734103b-5fa2-4358-8892-16ef8b23ccbd");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4600aabb-64c1-4247-9b05-8a368558b39d");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Receipts");

            migrationBuilder.RenameTable(
                name: "InvoiceSender",
                newName: "InvoiceSenders");

            migrationBuilder.AddColumn<string>(
                name: "BottomNotice",
                table: "InvoiceSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "InvoiceSenders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceSenders",
                table: "InvoiceSenders",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d0b3b958-cbc1-4cec-8f0c-8f823cbfad65", null, "Admin", "ADMIN" },
                    { "f137d46d-47e5-4c6a-a61b-6189d6341c8c", null, "Member", "MEMBER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceSenders_SenderId",
                table: "Invoices",
                column: "SenderId",
                principalTable: "InvoiceSenders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceSenders_SenderId",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceSenders",
                table: "InvoiceSenders");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d0b3b958-cbc1-4cec-8f0c-8f823cbfad65");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f137d46d-47e5-4c6a-a61b-6189d6341c8c");

            migrationBuilder.DropColumn(
                name: "BottomNotice",
                table: "InvoiceSettings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InvoiceSenders");

            migrationBuilder.RenameTable(
                name: "InvoiceSenders",
                newName: "InvoiceSender");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceSender",
                table: "InvoiceSender",
                column: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3734103b-5fa2-4358-8892-16ef8b23ccbd", null, "Member", "MEMBER" },
                    { "4600aabb-64c1-4247-9b05-8a368558b39d", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceSender_SenderId",
                table: "Invoices",
                column: "SenderId",
                principalTable: "InvoiceSender",
                principalColumn: "Id");
        }
    }
}
