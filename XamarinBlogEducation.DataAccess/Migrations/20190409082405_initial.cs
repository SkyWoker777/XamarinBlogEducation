using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XamarinBlogEducation.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Catgories_CategoryId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catgories",
                table: "Catgories");

            migrationBuilder.RenameTable(
                name: "Catgories",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Posts",
                newName: "AuthorName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    TagName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    PostId1 = table.Column<long>(nullable: true),
                    TagId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId1",
                        column: x => x.PostId1,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_TagId1",
                        column: x => x.TagId1,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostId1",
                table: "PostTags",
                column: "PostId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagId1",
                table: "PostTags",
                column: "TagId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Categories_CategoryId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Catgories");

            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Posts",
                newName: "Author");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catgories",
                table: "Catgories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Catgories_CategoryId",
                table: "Posts",
                column: "CategoryId",
                principalTable: "Catgories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
