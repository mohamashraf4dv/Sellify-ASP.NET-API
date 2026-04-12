using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sellify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingApplicationusernametoreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserName",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserName",
                table: "Reviews");
        }
    }
}
