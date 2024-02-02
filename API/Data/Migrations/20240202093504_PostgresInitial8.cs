using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class PostgresInitial8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9ff8285a-9dcc-42e6-8938-4fc7981a0fe7");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a4603fc2-9fde-4201-a861-ac58c4c6a1ee");

            migrationBuilder.RenameColumn(
                name: "InvoiceID",
                table: "Invoices",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiptId",
                table: "Products",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Invoices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Invoices",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "BottomNotice",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientCustomerID",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SettingsId",
                table: "Invoices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InvoiceSender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Company = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Zip = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceSender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    Locale = table.Column<string>(type: "text", nullable: true),
                    TaxNotation = table.Column<string>(type: "text", nullable: true),
                    MarginTop = table.Column<double>(type: "double precision", nullable: true),
                    MarginRight = table.Column<double>(type: "double precision", nullable: true),
                    MarginLeft = table.Column<double>(type: "double precision", nullable: true),
                    MarginBottom = table.Column<double>(type: "double precision", nullable: true),
                    Format = table.Column<string>(type: "text", nullable: true),
                    Height = table.Column<string>(type: "text", nullable: true),
                    Width = table.Column<string>(type: "text", nullable: true),
                    Orientation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptSender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Company = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Zip = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptSender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    Locale = table.Column<string>(type: "text", nullable: true),
                    TaxNotation = table.Column<string>(type: "text", nullable: true),
                    MarginTop = table.Column<double>(type: "double precision", nullable: true),
                    MarginRight = table.Column<double>(type: "double precision", nullable: true),
                    MarginLeft = table.Column<double>(type: "double precision", nullable: true),
                    MarginBottom = table.Column<double>(type: "double precision", nullable: true),
                    Format = table.Column<string>(type: "text", nullable: true),
                    Height = table.Column<string>(type: "text", nullable: true),
                    Width = table.Column<string>(type: "text", nullable: true),
                    Orientation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<string>(type: "text", nullable: true),
                    BottomNotice = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrderID = table.Column<int>(type: "integer", nullable: false),
                    SettingsId = table.Column<int>(type: "integer", nullable: true),
                    ClientCustomerID = table.Column<int>(type: "integer", nullable: true),
                    SenderId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Customers_ClientCustomerID",
                        column: x => x.ClientCustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_Receipts_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_ReceiptSender_SenderId",
                        column: x => x.SenderId,
                        principalTable: "ReceiptSender",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Receipts_ReceiptSettings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "ReceiptSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "126308fd-1196-4885-b84b-5889f70f600b", null, "Admin", "ADMIN" },
                    { "47b44acf-2b8a-4712-9153-1253f923eb34", null, "Member", "MEMBER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_InvoiceId",
                table: "Products",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ReceiptId",
                table: "Products",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientCustomerID",
                table: "Invoices",
                column: "ClientCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SenderId",
                table: "Invoices",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SettingsId",
                table: "Invoices",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ClientCustomerID",
                table: "Receipts",
                column: "ClientCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_OrderID",
                table: "Receipts",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SenderId",
                table: "Receipts",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SettingsId",
                table: "Receipts",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_UserId",
                table: "Receipts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_UserId",
                table: "Invoices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_ClientCustomerID",
                table: "Invoices",
                column: "ClientCustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceSender_SenderId",
                table: "Invoices",
                column: "SenderId",
                principalTable: "InvoiceSender",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_InvoiceSettings_SettingsId",
                table: "Invoices",
                column: "SettingsId",
                principalTable: "InvoiceSettings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Invoices_InvoiceId",
                table: "Products",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Receipts_ReceiptId",
                table: "Products",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_UserId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_ClientCustomerID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceSender_SenderId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_InvoiceSettings_SettingsId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Invoices_InvoiceId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Receipts_ReceiptId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "InvoiceSender");

            migrationBuilder.DropTable(
                name: "InvoiceSettings");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "ReceiptSender");

            migrationBuilder.DropTable(
                name: "ReceiptSettings");

            migrationBuilder.DropIndex(
                name: "IX_Products_InvoiceId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ReceiptId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ClientCustomerID",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_SenderId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_SettingsId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "126308fd-1196-4885-b84b-5889f70f600b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "47b44acf-2b8a-4712-9153-1253f923eb34");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BottomNotice",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ClientCustomerID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SettingsId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Invoices",
                newName: "InvoiceID");

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceID",
                table: "Invoices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "InvoiceID");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9ff8285a-9dcc-42e6-8938-4fc7981a0fe7", null, "Member", "MEMBER" },
                    { "a4603fc2-9fde-4201-a861-ac58c4c6a1ee", null, "Admin", "ADMIN" }
                });
        }
    }
}
