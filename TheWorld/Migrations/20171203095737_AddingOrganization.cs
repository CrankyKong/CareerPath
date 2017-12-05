using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheWorld.Migrations
{
    public partial class AddingOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Stops",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stops_OrganizationId",
                table: "Stops",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Organization_OrganizationId",
                table: "Stops",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Organization_OrganizationId",
                table: "Stops");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropIndex(
                name: "IX_Stops_OrganizationId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Stops");
        }
    }
}
