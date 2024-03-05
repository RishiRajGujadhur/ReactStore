using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class storeEntry2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "f9877700-e0df-4219-aeb7-6ec6e15c747f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fbf3cfdf-6e65-4f7d-98ab-58ae025b5a88");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "Warehouses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Warehouses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Warehouses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "Warehouses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "Warehouses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "Warehouses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "Reviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Reviews",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "Reviews",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "Reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "Reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "ReturnRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ReturnRequests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "ReturnRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "ReturnRequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "ReturnRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "ReturnRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Receipts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Receipts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "Receipts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "Receipts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "InvoiceSettings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "InvoiceSettings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "InvoiceSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "InvoiceSettings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "InvoiceSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "InvoiceSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "InvoiceSenders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "InvoiceSenders",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "InvoiceSenders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "InvoiceSenders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "InvoiceSenders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "InvoiceSenders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "Inventories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Inventories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "Inventories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "Inventories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "Inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "GeneralSettings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "GeneralSettings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "GeneralSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "GeneralSettings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "GeneralSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "GeneralSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "FeatureSettings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "FeatureSettings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "FeatureSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "FeatureSettings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "FeatureSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "FeatureSettings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Customers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "Customers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtTimestamp",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Comments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserName",
                table: "Comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedTimestamp",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedUserId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedUserName",
                table: "Comments",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b419683e-4f20-4058-bdbd-7e1192115ec2", null, "Admin", "ADMIN" },
                    { "d4bc839c-f238-494d-863d-8bb6aaeee070", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b419683e-4f20-4058-bdbd-7e1192115ec2");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d4bc839c-f238-494d-863d-8bb6aaeee070");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "ReturnRequests");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "InvoiceSettings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "InvoiceSettings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "InvoiceSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "InvoiceSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "InvoiceSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "InvoiceSettings");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "InvoiceSenders");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "InvoiceSenders");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "InvoiceSenders");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "InvoiceSenders");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "InvoiceSenders");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "InvoiceSenders");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "FeatureSettings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "FeatureSettings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "FeatureSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "FeatureSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "FeatureSettings");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "FeatureSettings");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedAtTimestamp",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedByUserName",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastModifiedTimestamp",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastModifiedUserName",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f9877700-e0df-4219-aeb7-6ec6e15c747f", null, "Member", "MEMBER" },
                    { "fbf3cfdf-6e65-4f7d-98ab-58ae025b5a88", null, "Admin", "ADMIN" }
                });
        }
    }
}
