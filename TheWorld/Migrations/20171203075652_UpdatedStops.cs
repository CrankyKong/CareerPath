using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheWorld.Migrations
{
    public partial class UpdatedStops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "Stops",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Wishlist",
                table: "Stops",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "Wishlist",
                table: "Stops");
        }
    }
}
