﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BBC.Infrastructure.Migrations
{
    public partial class add_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Popularity_AspNetUsers_UserId",
                table: "Popularity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Popularity",
                table: "Popularity");

            migrationBuilder.RenameTable(
                name: "Popularity",
                newName: "Popularities");

            migrationBuilder.RenameIndex(
                name: "IX_Popularity_UserId",
                table: "Popularities",
                newName: "IX_Popularities_UserId");

            migrationBuilder.AddColumn<int>(
                name: "TarifAndReceteId1",
                table: "Popularities",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Popularities",
                table: "Popularities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Popularities_TarifAndReceteId1",
                table: "Popularities",
                column: "TarifAndReceteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Popularities_TarifAndRecetes_TarifAndReceteId1",
                table: "Popularities",
                column: "TarifAndReceteId1",
                principalTable: "TarifAndRecetes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Popularities_AspNetUsers_UserId",
                table: "Popularities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Popularities_TarifAndRecetes_TarifAndReceteId1",
                table: "Popularities");

            migrationBuilder.DropForeignKey(
                name: "FK_Popularities_AspNetUsers_UserId",
                table: "Popularities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Popularities",
                table: "Popularities");

            migrationBuilder.DropIndex(
                name: "IX_Popularities_TarifAndReceteId1",
                table: "Popularities");

            migrationBuilder.DropColumn(
                name: "TarifAndReceteId1",
                table: "Popularities");

            migrationBuilder.RenameTable(
                name: "Popularities",
                newName: "Popularity");

            migrationBuilder.RenameIndex(
                name: "IX_Popularities_UserId",
                table: "Popularity",
                newName: "IX_Popularity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Popularity",
                table: "Popularity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Popularity_AspNetUsers_UserId",
                table: "Popularity",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
