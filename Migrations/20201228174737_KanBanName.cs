using Microsoft.EntityFrameworkCore.Migrations;

namespace KanbanApi.Migrations
{
    public partial class KanBanName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Kanbans",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Kanbans");
        }
    }
}
