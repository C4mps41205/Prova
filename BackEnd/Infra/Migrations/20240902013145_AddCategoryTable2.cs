using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskToDo_Cateogory_CategoryId",
                table: "TaskToDo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cateogory",
                table: "Cateogory");

            migrationBuilder.RenameTable(
                name: "Cateogory",
                newName: "Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskToDo_Category_CategoryId",
                table: "TaskToDo",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskToDo_Category_CategoryId",
                table: "TaskToDo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Cateogory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cateogory",
                table: "Cateogory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskToDo_Cateogory_CategoryId",
                table: "TaskToDo",
                column: "CategoryId",
                principalTable: "Cateogory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
