using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sellify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixcirculardependencyinproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductImage_ThumbnailId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ThumbnailId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailSource",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsThumbnail",
                table: "ProductImage",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailSource",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsThumbnail",
                table: "ProductImage");

            migrationBuilder.AddColumn<Guid>(
                name: "ThumbnailId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductImage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ThumbnailId",
                table: "Products",
                column: "ThumbnailId",
                unique: true,
                filter: "[ThumbnailId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductImage_ThumbnailId",
                table: "Products",
                column: "ThumbnailId",
                principalTable: "ProductImage",
                principalColumn: "Id");
        }
    }
}
