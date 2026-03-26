using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sellify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0F9E7582-B7A3-4A6F-B1CC-79F2F350C2FF", "0F9E7582-B7A3-4A6F-B1CC-79F2F350C2FF", "Seller", "SELLER" },
                    { "C8CA233C-8C42-433D-A3ED-D5A8A2E7FC77", "C8CA233C-8C42-433D-A3ED-D5A8A2E7FC77", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0F9E7582-B7A3-4A6F-B1CC-79F2F350C2FF");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "C8CA233C-8C42-433D-A3ED-D5A8A2E7FC77");
        }
    }
}
