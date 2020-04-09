using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBC.Infrastructure.Migrations
{
    public partial class remove_content_relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Categories_CategoryId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_CategoryId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "DeletedTime",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Contents");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "TarifAndRecetes",
                newName: "isDeleted");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "TarifAndRecetes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreateUserId",
                table: "TarifAndRecetes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeleteId",
                table: "TarifAndRecetes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteTime",
                table: "TarifAndRecetes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "TarifAndRecetes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdateUserId",
                table: "TarifAndRecetes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "TarifAndRecetes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "DeleteTime",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "TarifAndRecetes");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "TarifAndRecetes",
                newName: "IsDeleted");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "TarifAndRecetes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTime",
                table: "TarifAndRecetes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "TarifAndRecetes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Contents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contents_CategoryId",
                table: "Contents",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Categories_CategoryId",
                table: "Contents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
