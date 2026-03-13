using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sellify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initsellerwithtotalearned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalEarned",
                table: "Seller",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalEarned",
                table: "Seller");
        }
    }
}
