using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheWorld.Migrations
{
    public partial class JobTitlesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobTitleId",
                table: "Stops",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stops_JobTitleId",
                table: "Stops",
                column: "JobTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_JobTitles_JobTitleId",
                table: "Stops",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_JobTitles_JobTitleId",
                table: "Stops");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropIndex(
                name: "IX_Stops_JobTitleId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "JobTitleId",
                table: "Stops");
        }
    }
}
