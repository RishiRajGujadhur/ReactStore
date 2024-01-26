using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class IdForRedux : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c5e8c350-ec97-4e39-92f0-cdaa9dd02bde");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e0c14dee-08dd-4141-9232-43bd0fb5f9d3");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9ff8285a-9dcc-42e6-8938-4fc7981a0fe7", null, "Member", "MEMBER" },
                    { "a4603fc2-9fde-4201-a861-ac58c4c6a1ee", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9ff8285a-9dcc-42e6-8938-4fc7981a0fe7");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a4603fc2-9fde-4201-a861-ac58c4c6a1ee");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c5e8c350-ec97-4e39-92f0-cdaa9dd02bde", null, "Member", "MEMBER" },
                    { "e0c14dee-08dd-4141-9232-43bd0fb5f9d3", null, "Admin", "ADMIN" }
                });
        }
    }
}
