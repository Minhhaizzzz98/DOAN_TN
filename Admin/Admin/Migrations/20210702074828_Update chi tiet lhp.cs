using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Migrations
{
    public partial class Updatechitietlhp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TrangThai",
                table: "LopHocPhans",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai",
                table: "LopHocPhans",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
