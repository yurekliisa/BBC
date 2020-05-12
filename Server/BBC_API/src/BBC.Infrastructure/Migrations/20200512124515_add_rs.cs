using Microsoft.EntityFrameworkCore.Migrations;

namespace BBC.Infrastructure.Migrations
{
    public partial class add_rs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Popularities_TarifAndRecetes_TarifAndReceteId1",
                table: "Popularities");

            migrationBuilder.DropIndex(
                name: "IX_Popularities_TarifAndReceteId1",
                table: "Popularities");

            migrationBuilder.DropColumn(
                name: "TarifAndReceteId1",
                table: "Popularities");

            migrationBuilder.AddColumn<int>(
                name: "TarifAndReceteId",
                table: "Popularities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Popularities_TarifAndReceteId",
                table: "Popularities",
                column: "TarifAndReceteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Popularities_TarifAndRecetes_TarifAndReceteId",
                table: "Popularities",
                column: "TarifAndReceteId",
                principalTable: "TarifAndRecetes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Popularities_TarifAndRecetes_TarifAndReceteId",
                table: "Popularities");

            migrationBuilder.DropIndex(
                name: "IX_Popularities_TarifAndReceteId",
                table: "Popularities");

            migrationBuilder.DropColumn(
                name: "TarifAndReceteId",
                table: "Popularities");

            migrationBuilder.AddColumn<int>(
                name: "TarifAndReceteId1",
                table: "Popularities",
                type: "int",
                nullable: true);

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
        }
    }
}
