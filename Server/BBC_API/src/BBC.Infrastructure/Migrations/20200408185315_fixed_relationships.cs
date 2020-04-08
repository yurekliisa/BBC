using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBC.Infrastructure.Migrations
{
    public partial class fixed_relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_LobiMessages_Lobis_LobiId",
                table: "LobiMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TarifAndRecetes_Contents_ContentId",
                table: "TarifAndRecetes");

            migrationBuilder.DropForeignKey(
                name: "FK_TarifAndRecetes_Popularities_PopularityId",
                table: "TarifAndRecetes");

            migrationBuilder.DropTable(
                name: "JobUsers");

            migrationBuilder.DropTable(
                name: "Templates");

            migrationBuilder.DropTable(
                name: "ToRCategories");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_TarifAndRecetes_ContentId",
                table: "TarifAndRecetes");

            migrationBuilder.DropIndex(
                name: "IX_TarifAndRecetes_PopularityId",
                table: "TarifAndRecetes");

            migrationBuilder.DropIndex(
                name: "IX_LobiMessages_LobiId",
                table: "LobiMessages");

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
                name: "ContentId",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "IsRecete",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "IsTarif",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "PopularityId",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "ToRId",
                table: "Popularities");

            migrationBuilder.DropColumn(
                name: "LobiId",
                table: "LobiMessages");

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
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SocialMedias",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PrivateMessages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TarifAndReceteId",
                table: "Popularities",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TaRId",
                table: "Popularities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Contents",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarifandReceteId",
                table: "Contents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobAdverts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeletedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    JobsApplicationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAdverts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobAdverts_JobsApplications_JobsApplicationId",
                        column: x => x.JobsApplicationId,
                        principalTable: "JobsApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobAdverts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaRCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    TarifAndReceteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaRCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaRCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaRCategories_TarifAndRecetes_TarifAndReceteId",
                        column: x => x.TarifAndReceteId,
                        principalTable: "TarifAndRecetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TarifAndRecetes_UserId",
                table: "TarifAndRecetes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedias_UserId",
                table: "SocialMedias",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMessages_UserId",
                table: "PrivateMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Popularities_TarifAndReceteId",
                table: "Popularities",
                column: "TarifAndReceteId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_ContentId",
                table: "Medias",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_LobiMessages_FromUserId",
                table: "LobiMessages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LobiMessages_ToLobiId",
                table: "LobiMessages",
                column: "ToLobiId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_TarifandReceteId",
                table: "Contents",
                column: "TarifandReceteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobAdverts_JobsApplicationId",
                table: "JobAdverts",
                column: "JobsApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdverts_UserId",
                table: "JobAdverts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaRCategories_CategoryId",
                table: "TaRCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TaRCategories_TarifAndReceteId",
                table: "TaRCategories",
                column: "TarifAndReceteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Categories_CategoryId",
                table: "Contents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_TarifAndRecetes_TarifandReceteId",
                table: "Contents",
                column: "TarifandReceteId",
                principalTable: "TarifAndRecetes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LobiMessages_AspNetUsers_FromUserId",
                table: "LobiMessages",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LobiMessages_Lobis_ToLobiId",
                table: "LobiMessages",
                column: "ToLobiId",
                principalTable: "Lobis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medias_Contents_ContentId",
                table: "Medias",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Popularities_TarifAndRecetes_TarifAndReceteId",
                table: "Popularities",
                column: "TarifAndReceteId",
                principalTable: "TarifAndRecetes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateMessages_AspNetUsers_UserId",
                table: "PrivateMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedias_AspNetUsers_UserId",
                table: "SocialMedias",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TarifAndRecetes_AspNetUsers_UserId",
                table: "TarifAndRecetes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Categories_CategoryId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_TarifAndRecetes_TarifandReceteId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_LobiMessages_AspNetUsers_FromUserId",
                table: "LobiMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_LobiMessages_Lobis_ToLobiId",
                table: "LobiMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_Medias_Contents_ContentId",
                table: "Medias");

            migrationBuilder.DropForeignKey(
                name: "FK_Popularities_TarifAndRecetes_TarifAndReceteId",
                table: "Popularities");

            migrationBuilder.DropForeignKey(
                name: "FK_PrivateMessages_AspNetUsers_UserId",
                table: "PrivateMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedias_AspNetUsers_UserId",
                table: "SocialMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_TarifAndRecetes_AspNetUsers_UserId",
                table: "TarifAndRecetes");

            migrationBuilder.DropTable(
                name: "JobAdverts");

            migrationBuilder.DropTable(
                name: "TaRCategories");

            migrationBuilder.DropIndex(
                name: "IX_TarifAndRecetes_UserId",
                table: "TarifAndRecetes");

            migrationBuilder.DropIndex(
                name: "IX_SocialMedias_UserId",
                table: "SocialMedias");

            migrationBuilder.DropIndex(
                name: "IX_PrivateMessages_UserId",
                table: "PrivateMessages");

            migrationBuilder.DropIndex(
                name: "IX_Popularities_TarifAndReceteId",
                table: "Popularities");

            migrationBuilder.DropIndex(
                name: "IX_Medias_ContentId",
                table: "Medias");

            migrationBuilder.DropIndex(
                name: "IX_LobiMessages_FromUserId",
                table: "LobiMessages");

            migrationBuilder.DropIndex(
                name: "IX_LobiMessages_ToLobiId",
                table: "LobiMessages");

            migrationBuilder.DropIndex(
                name: "IX_Contents_TarifandReceteId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TarifAndRecetes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PrivateMessages");

            migrationBuilder.DropColumn(
                name: "TaRId",
                table: "Popularities");

            migrationBuilder.DropColumn(
                name: "TarifandReceteId",
                table: "Contents");

            migrationBuilder.AddColumn<int>(
                name: "ContentId",
                table: "TarifAndRecetes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecete",
                table: "TarifAndRecetes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTarif",
                table: "TarifAndRecetes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PopularityId",
                table: "TarifAndRecetes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "TarifAndRecetes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TarifAndReceteId",
                table: "Popularities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToRId",
                table: "Popularities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LobiId",
                table: "LobiMessages",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Contents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "Contents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LobiMessagesId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrivateMessagesId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SocialMediaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TarifAndReceteId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    JobsApplicationId = table.Column<int>(type: "int", nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_JobsApplications_JobsApplicationId",
                        column: x => x.JobsApplicationId,
                        principalTable: "JobsApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TarifAndReceteId = table.Column<int>(type: "int", nullable: true),
                    TemplateData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Templates_TarifAndRecetes_TarifAndReceteId",
                        column: x => x.TarifAndReceteId,
                        principalTable: "TarifAndRecetes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToRCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TarifAndReceteId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "JobUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_LobiMessages_LobiId",
                table: "LobiMessages",
                column: "LobiId");

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
                name: "IX_Jobs_JobsApplicationId",
                table: "Jobs",
                column: "JobsApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobUsers_JobId",
                table: "JobUsers",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobUsers_UserId",
                table: "JobUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_TarifAndReceteId",
                table: "Templates",
                column: "TarifAndReceteId");

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
                name: "FK_LobiMessages_Lobis_LobiId",
                table: "LobiMessages",
                column: "LobiId",
                principalTable: "Lobis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
        }
    }
}
