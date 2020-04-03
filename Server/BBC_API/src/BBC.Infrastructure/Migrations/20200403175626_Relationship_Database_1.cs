using Microsoft.EntityFrameworkCore.Migrations;

namespace BBC.Infrastructure.Migrations
{
    public partial class Relationship_Database_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobsApplications");

            migrationBuilder.AddColumn<int>(
                name: "TarifAndReceteId",
                table: "Templates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PopularityId",
                table: "TarifAndRecetes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LobiId",
                table: "LobiMessages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobsApplicationId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LobiMessagesId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrivateMessagesId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SocialMediaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarifAndReceteId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobUsers_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToRCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    TarifAndReceteId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToRCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToRCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToRCategories_TarifAndRecetes_TarifAndReceteId",
                        column: x => x.TarifAndReceteId,
                        principalTable: "TarifAndRecetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToRCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TarifAndReceteId",
                table: "Templates",
                column: "TarifAndReceteId");

            migrationBuilder.CreateIndex(
                name: "IX_TarifAndRecetes_ContentId",
                table: "TarifAndRecetes",
                column: "ContentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TarifAndRecetes_PopularityId",
                table: "TarifAndRecetes",
                column: "PopularityId");

            migrationBuilder.CreateIndex(
                name: "IX_Popularities_UserId",
                table: "Popularities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LobiUsers_LobiId",
                table: "LobiUsers",
                column: "LobiId");

            migrationBuilder.CreateIndex(
                name: "IX_LobiUsers_UserId",
                table: "LobiUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LobiMessages_LobiId",
                table: "LobiMessages",
                column: "LobiId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobsApplicationId",
                table: "Jobs",
                column: "JobsApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_CategoryId",
                table: "Contents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_MediaId",
                table: "Contents",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LobiMessagesId",
                table: "AspNetUsers",
                column: "LobiMessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PrivateMessagesId",
                table: "AspNetUsers",
                column: "PrivateMessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SocialMediaId",
                table: "AspNetUsers",
                column: "SocialMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TarifAndReceteId",
                table: "AspNetUsers",
                column: "TarifAndReceteId");

            migrationBuilder.CreateIndex(
                name: "IX_JobUsers_JobId",
                table: "JobUsers",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobUsers_UserId",
                table: "JobUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ToRCategories_CategoryId",
                table: "ToRCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ToRCategories_TarifAndReceteId",
                table: "ToRCategories",
                column: "TarifAndReceteId");

            migrationBuilder.CreateIndex(
                name: "IX_ToRCategories_UserId",
                table: "ToRCategories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LobiMessages_LobiMessagesId",
                table: "AspNetUsers",
                column: "LobiMessagesId",
                principalTable: "LobiMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PrivateMessages_PrivateMessagesId",
                table: "AspNetUsers",
                column: "PrivateMessagesId",
                principalTable: "PrivateMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SocialMedias_SocialMediaId",
                table: "AspNetUsers",
                column: "SocialMediaId",
                principalTable: "SocialMedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TarifAndRecetes_TarifAndReceteId",
                table: "AspNetUsers",
                column: "TarifAndReceteId",
                principalTable: "TarifAndRecetes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Categories_CategoryId",
                table: "Contents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Medias_MediaId",
                table: "Contents",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobsApplications_JobsApplicationId",
                table: "Jobs",
                column: "JobsApplicationId",
                principalTable: "JobsApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LobiMessages_Lobis_LobiId",
                table: "LobiMessages",
                column: "LobiId",
                principalTable: "Lobis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LobiUsers_Lobis_LobiId",
                table: "LobiUsers",
                column: "LobiId",
                principalTable: "Lobis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LobiUsers_AspNetUsers_UserId",
                table: "LobiUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Popularities_AspNetUsers_UserId",
                table: "Popularities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TarifAndRecetes_Contents_ContentId",
                table: "TarifAndRecetes",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TarifAndRecetes_Popularities_PopularityId",
                table: "TarifAndRecetes",
                column: "PopularityId",
                principalTable: "Popularities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_TarifAndRecetes_TarifAndReceteId",
                table: "Templates",
                column: "TarifAndReceteId",
                principalTable: "TarifAndRecetes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LobiMessages_LobiMessagesId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PrivateMessages_PrivateMessagesId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SocialMedias_SocialMediaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TarifAndRecetes_TarifAndReceteId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Categories_CategoryId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Medias_MediaId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobsApplications_JobsApplicationId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_LobiMessages_Lobis_LobiId",
                table: "LobiMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_LobiUsers_Lobis_LobiId",
                table: "LobiUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_LobiUsers_AspNetUsers_UserId",
                table: "LobiUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Popularities_AspNetUsers_UserId",
                table: "Popularities");

            migrationBuilder.DropForeignKey(
                name: "FK_TarifAndRecetes_Contents_ContentId",
                table: "TarifAndRecetes");

            migrationBuilder.DropForeignKey(
                name: "FK_TarifAndRecetes_Popularities_PopularityId",
                table: "TarifAndRecetes");

            migrationBuilder.DropForeignKey(
                name: "FK_Templates_TarifAndRecetes_TarifAndReceteId",
                table: "Templates");

            migrationBuilder.DropTable(
                name: "JobUsers");

            migrationBuilder.DropTable(
                name: "ToRCategories");

            migrationBuilder.DropIndex(
                name: "IX_Templates_TarifAndReceteId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_TarifAndRecetes_ContentId",
                table: "TarifAndRecetes");

            migrationBuilder.DropIndex(
                name: "IX_TarifAndRecetes_PopularityId",
                table: "TarifAndRecetes");

            migrationBuilder.DropIndex(
                name: "IX_Popularities_UserId",
                table: "Popularities");

            migrationBuilder.DropIndex(
                name: "IX_LobiUsers_LobiId",
                table: "LobiUsers");

            migrationBuilder.DropIndex(
                name: "IX_LobiUsers_UserId",
                table: "LobiUsers");

            migrationBuilder.DropIndex(
                name: "IX_LobiMessages_LobiId",
                table: "LobiMessages");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobsApplicationId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Contents_CategoryId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_MediaId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LobiMessagesId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PrivateMessagesId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SocialMediaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TarifAndReceteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TarifAndReceteId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "PopularityId",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "LobiId",
                table: "LobiMessages");

            migrationBuilder.DropColumn(
                name: "JobsApplicationId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "LobiMessagesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PrivateMessagesId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SocialMediaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TarifAndReceteId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TarifAndRecetes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "JobsApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
