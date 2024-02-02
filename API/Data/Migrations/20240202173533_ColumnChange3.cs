using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnChange3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Customers_CustomerID",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_CustomerID",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_CustomerID",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7c67862f-abb1-4151-8b86-533f78b22f56");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "98893e86-dbba-45a8-adcc-bc8da3672dde");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Invoices");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1be9509d-cd7b-4239-9a87-0c8c35a73973", null, "Member", "MEMBER" },
                    { "82fa586e-7d40-488b-a4e6-c0514221461e", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1be9509d-cd7b-4239-9a87-0c8c35a73973");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "82fa586e-7d40-488b-a4e6-c0514221461e");

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Receipts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c67862f-abb1-4151-8b86-533f78b22f56", null, "Admin", "ADMIN" },
                    { "98893e86-dbba-45a8-adcc-bc8da3672dde", null, "Member", "MEMBER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CustomerID",
                table: "Receipts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerID",
                table: "Invoices",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerID",
                table: "Invoices",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Customers_CustomerID",
                table: "Receipts",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");
        }
    }
}
