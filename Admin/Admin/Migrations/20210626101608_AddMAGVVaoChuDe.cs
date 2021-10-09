using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Migrations
{
    public partial class AddMAGVVaoChuDe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaGV",
                table: "ChuDes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaGV",
                table: "ChuDes");
        }
    }
}
