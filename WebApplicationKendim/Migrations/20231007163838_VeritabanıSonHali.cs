using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationKendim.Migrations
{
    /// <inheritdoc />
    public partial class VeritabanıSonHali : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KitapTurleriTablosu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitapTurleriTablosu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KitapBilgileriTablosu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KitapAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tanim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Yazar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fiyat = table.Column<double>(type: "float", nullable: false),
                    KitapTuruId = table.Column<int>(type: "int", nullable: false),
                    ResimUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitapBilgileriTablosu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitapBilgileriTablosu_KitapTurleriTablosu_KitapTuruId",
                        column: x => x.KitapTuruId,
                        principalTable: "KitapTurleriTablosu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KitapBilgileriTablosu_KitapTuruId",
                table: "KitapBilgileriTablosu",
                column: "KitapTuruId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KitapBilgileriTablosu");

            migrationBuilder.DropTable(
                name: "KitapTurleriTablosu");
        }
    }
}
