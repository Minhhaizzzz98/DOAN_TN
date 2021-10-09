using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Migrations
{
    public partial class updateKetQua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoCauDung",
                table: "KetQuas",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoCauDung",
                table: "KetQuas");
        }
    }
}
