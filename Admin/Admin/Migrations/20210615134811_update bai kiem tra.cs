using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Migrations
{
    public partial class updatebaikiemtra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnd",
                table: "BaiKiemTras",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnd",
                table: "BaiKiemTras");
        }
    }
}
