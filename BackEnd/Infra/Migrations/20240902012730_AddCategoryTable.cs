using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "TaskToDo");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "TaskToDo",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cateogory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cateogory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskToDo_CategoryId",
                table: "TaskToDo",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskToDo_Cateogory_CategoryId",
                table: "TaskToDo",
                column: "CategoryId",
                principalTable: "Cateogory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskToDo_Cateogory_CategoryId",
                table: "TaskToDo");

            migrationBuilder.DropTable(
                name: "Cateogory");

            migrationBuilder.DropIndex(
                name: "IX_TaskToDo_CategoryId",
                table: "TaskToDo");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "TaskToDo");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "TaskToDo",
                type: "text",
                nullable: true);
        }
    }
}
