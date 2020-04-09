using Microsoft.EntityFrameworkCore.Migrations;

namespace BBC.Infrastructure.Migrations
{
    public partial class edited_relationships_jobsadvert_jobsapp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdverts_JobsApplications_JobsApplicationId",
                table: "JobAdverts");

            migrationBuilder.DropIndex(
                name: "IX_JobAdverts_JobsApplicationId",
                table: "JobAdverts");

            migrationBuilder.DropColumn(
                name: "JobsApplicationId",
                table: "JobAdverts");

            migrationBuilder.AddColumn<int>(
                name: "JobAdvertId",
                table: "JobsApplications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobsApplications_JobAdvertId",
                table: "JobsApplications",
                column: "JobAdvertId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobsApplications_JobAdverts_JobAdvertId",
                table: "JobsApplications",
                column: "JobAdvertId",
                principalTable: "JobAdverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobsApplications_JobAdverts_JobAdvertId",
                table: "JobsApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobsApplications_JobAdvertId",
                table: "JobsApplications");

            migrationBuilder.DropColumn(
                name: "JobAdvertId",
                table: "JobsApplications");

            migrationBuilder.AddColumn<int>(
                name: "JobsApplicationId",
                table: "JobAdverts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobAdverts_JobsApplicationId",
                table: "JobAdverts",
                column: "JobsApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdverts_JobsApplications_JobsApplicationId",
                table: "JobAdverts",
                column: "JobsApplicationId",
                principalTable: "JobsApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
