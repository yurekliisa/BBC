using Microsoft.EntityFrameworkCore.Migrations;

namespace BBC.Infrastructure.Migrations
{
    public partial class Fixed_MM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateMessages_AspNetUsers_UserId",
                table: "PrivateMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaRCategories",
                table: "TaRCategories");

            migrationBuilder.DropIndex(
                name: "IX_TaRCategories_CategoryId",
                table: "TaRCategories");

            migrationBuilder.DropIndex(
                name: "IX_PrivateMessages_UserId",
                table: "PrivateMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LobiUsers",
                table: "LobiUsers");

            migrationBuilder.DropIndex(
                name: "IX_LobiUsers_LobiId",
                table: "LobiUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TaRCategories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PrivateMessages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LobiUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaRCategories",
                table: "TaRCategories",
                columns: new[] { "CategoryId", "TarifAndReceteId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LobiUsers",
                table: "LobiUsers",
                columns: new[] { "LobiId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMessages_FromUserId",
                table: "PrivateMessages",
                column: "FromUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateMessages_AspNetUsers_FromUserId",
                table: "PrivateMessages",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateMessages_AspNetUsers_FromUserId",
                table: "PrivateMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaRCategories",
                table: "TaRCategories");

            migrationBuilder.DropIndex(
                name: "IX_PrivateMessages_FromUserId",
                table: "PrivateMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LobiUsers",
                table: "LobiUsers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TaRCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PrivateMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LobiUsers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaRCategories",
                table: "TaRCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LobiUsers",
                table: "LobiUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TaRCategories_CategoryId",
                table: "TaRCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMessages_UserId",
                table: "PrivateMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LobiUsers_LobiId",
                table: "LobiUsers",
                column: "LobiId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateMessages_AspNetUsers_UserId",
                table: "PrivateMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
