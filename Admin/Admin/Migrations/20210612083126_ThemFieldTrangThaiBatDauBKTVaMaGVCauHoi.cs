using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Migrations
{
    public partial class ThemFieldTrangThaiBatDauBKTVaMaGVCauHoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaGV",
                table: "CauHois",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThaiBatDau",
                table: "BaiKiemTras",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaGV",
                table: "CauHois");

            migrationBuilder.DropColumn(
                name: "TrangThaiBatDau",
                table: "BaiKiemTras");
        }
    }
}
