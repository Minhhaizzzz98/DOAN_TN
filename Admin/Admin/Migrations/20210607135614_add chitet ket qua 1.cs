using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Admin.Migrations
{
    public partial class addchitetketqua1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaiKiemTras",
                columns: table => new
                {
                    MaBaiKT = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBaiKT = table.Column<string>(nullable: true),
                    KeyBaiKT = table.Column<string>(nullable: true),
                    Ngay = table.Column<DateTime>(nullable: false),
                    MaGiangVien = table.Column<int>(nullable: false),
                    MaLopHocPhan = table.Column<int>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false),
                    ThoiGianLam = table.Column<int>(nullable: false),
                    ThoiGianBatDau = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiKiemTras", x => x.MaBaiKT);
                });

            migrationBuilder.CreateTable(
                name: "CauHois",
                columns: table => new
                {
                    MaCauHoi = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCauHoi = table.Column<string>(nullable: true),
                    A = table.Column<string>(nullable: true),
                    B = table.Column<string>(nullable: true),
                    C = table.Column<string>(nullable: true),
                    D = table.Column<string>(nullable: true),
                    DapAn = table.Column<string>(nullable: true),
                    MaChuDe = table.Column<int>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHois", x => x.MaCauHoi);
                });

            migrationBuilder.CreateTable(
                name: "ChuDes",
                columns: table => new
                {
                    MaChuDe = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChuDe = table.Column<string>(nullable: true),
                    MonHoc = table.Column<int>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuDes", x => x.MaChuDe);
                });

            migrationBuilder.CreateTable(
                name: "CTBaiKTs",
                columns: table => new
                {
                    MaCTBaiKT = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBaiKT = table.Column<int>(nullable: false),
                    CauHoi = table.Column<int>(nullable: false),
                    STT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTBaiKTs", x => x.MaCTBaiKT);
                });

            migrationBuilder.CreateTable(
                name: "GiangViens",
                columns: table => new
                {
                    MaGiangVien = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenGiangVien = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    SoDienThoai = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false),
                    MaTaiKhoan = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangViens", x => x.MaGiangVien);
                });

            migrationBuilder.CreateTable(
                name: "KetQuas",
                columns: table => new
                {
                    MaKetQua = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSinhVien = table.Column<int>(nullable: false),
                    MaBaiKiemTra = table.Column<int>(nullable: false),
                    Diem = table.Column<float>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetQuas", x => x.MaKetQua);
                });

            migrationBuilder.CreateTable(
                name: "LopHocPhans",
                columns: table => new
                {
                    MaLopHP = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLopHP = table.Column<string>(nullable: false),
                    MaGiangVien = table.Column<int>(nullable: false),
                    MaMonHoc = table.Column<int>(nullable: false),
                    MaLop = table.Column<int>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocPhans", x => x.MaLopHP);
                });

            migrationBuilder.CreateTable(
                name: "Lops",
                columns: table => new
                {
                    MaLop = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLop = table.Column<string>(nullable: true),
                    SoLuongSV = table.Column<int>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lops", x => x.MaLop);
                });

            migrationBuilder.CreateTable(
                name: "MonHocs",
                columns: table => new
                {
                    MaMonHoc = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMonHoc = table.Column<string>(nullable: false),
                    SoTinChi = table.Column<int>(nullable: false),
                    SoTiet = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHocs", x => x.MaMonHoc);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    MaSV = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSV = table.Column<string>(nullable: false),
                    DiaChi = table.Column<string>(nullable: false),
                    SoDienThoai = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Lop = table.Column<int>(nullable: false),
                    MaTaiKhoan = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.MaSV);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    LoaiTaiKhoan = table.Column<int>(nullable: false),
                    TrangThai = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CTKetQuas",
                columns: table => new
                {
                    MaCTKetQua = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SinhVienMaSV = table.Column<int>(nullable: false),
                    BaiKiemTraMaBaiKT = table.Column<int>(nullable: false),
                    CauHoiMaCauHoi = table.Column<int>(nullable: false),
                    DapAnSVChon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTKetQuas", x => x.MaCTKetQua);
                    table.ForeignKey(
                        name: "FK_CTKetQuas_BaiKiemTras_BaiKiemTraMaBaiKT",
                        column: x => x.BaiKiemTraMaBaiKT,
                        principalTable: "BaiKiemTras",
                        principalColumn: "MaBaiKT",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTKetQuas_CauHois_CauHoiMaCauHoi",
                        column: x => x.CauHoiMaCauHoi,
                        principalTable: "CauHois",
                        principalColumn: "MaCauHoi",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTKetQuas_SinhViens_SinhVienMaSV",
                        column: x => x.SinhVienMaSV,
                        principalTable: "SinhViens",
                        principalColumn: "MaSV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTLopHPs",
                columns: table => new
                {
                    MaCTLopHP = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLopHocPhan = table.Column<int>(nullable: false),
                    MaSinhVien = table.Column<int>(nullable: false),
                    SinhVienMaSV = table.Column<int>(nullable: false),
                    LopHocPhanMaLopHP = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTLopHPs", x => x.MaCTLopHP);
                    table.ForeignKey(
                        name: "FK_CTLopHPs_LopHocPhans_LopHocPhanMaLopHP",
                        column: x => x.LopHocPhanMaLopHP,
                        principalTable: "LopHocPhans",
                        principalColumn: "MaLopHP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTLopHPs_SinhViens_SinhVienMaSV",
                        column: x => x.SinhVienMaSV,
                        principalTable: "SinhViens",
                        principalColumn: "MaSV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTKetQuas_BaiKiemTraMaBaiKT",
                table: "CTKetQuas",
                column: "BaiKiemTraMaBaiKT");

            migrationBuilder.CreateIndex(
                name: "IX_CTKetQuas_CauHoiMaCauHoi",
                table: "CTKetQuas",
                column: "CauHoiMaCauHoi");

            migrationBuilder.CreateIndex(
                name: "IX_CTKetQuas_SinhVienMaSV",
                table: "CTKetQuas",
                column: "SinhVienMaSV");

            migrationBuilder.CreateIndex(
                name: "IX_CTLopHPs_LopHocPhanMaLopHP",
                table: "CTLopHPs",
                column: "LopHocPhanMaLopHP");

            migrationBuilder.CreateIndex(
                name: "IX_CTLopHPs_SinhVienMaSV",
                table: "CTLopHPs",
                column: "SinhVienMaSV");

            migrationBuilder.CreateIndex(
                name: "IX_Lops_TenLop",
                table: "Lops",
                column: "TenLop",
                unique: true,
                filter: "[TenLop] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChuDes");

            migrationBuilder.DropTable(
                name: "CTBaiKTs");

            migrationBuilder.DropTable(
                name: "CTKetQuas");

            migrationBuilder.DropTable(
                name: "CTLopHPs");

            migrationBuilder.DropTable(
                name: "GiangViens");

            migrationBuilder.DropTable(
                name: "KetQuas");

            migrationBuilder.DropTable(
                name: "Lops");

            migrationBuilder.DropTable(
                name: "MonHocs");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "BaiKiemTras");

            migrationBuilder.DropTable(
                name: "CauHois");

            migrationBuilder.DropTable(
                name: "LopHocPhans");

            migrationBuilder.DropTable(
                name: "SinhViens");
        }
    }
}
