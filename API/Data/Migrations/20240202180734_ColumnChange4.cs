using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnChange4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1be9509d-cd7b-4239-9a87-0c8c35a73973");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "82fa586e-7d40-488b-a4e6-c0514221461e");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3734103b-5fa2-4358-8892-16ef8b23ccbd", null, "Member", "MEMBER" },
                    { "4600aabb-64c1-4247-9b05-8a368558b39d", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "3734103b-5fa2-4358-8892-16ef8b23ccbd");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4600aabb-64c1-4247-9b05-8a368558b39d");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Receipts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1be9509d-cd7b-4239-9a87-0c8c35a73973", null, "Member", "MEMBER" },
                    { "82fa586e-7d40-488b-a4e6-c0514221461e", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");
        }
    }
}
