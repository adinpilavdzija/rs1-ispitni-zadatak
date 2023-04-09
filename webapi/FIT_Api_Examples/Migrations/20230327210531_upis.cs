using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class upis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpisAkGodina",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    godinaStudija = table.Column<int>(type: "int", nullable: false),
                    cijenaSkolarine = table.Column<float>(type: "real", nullable: false),
                    isObnova = table.Column<bool>(type: "bit", nullable: false),
                    datumOvjere = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ovjeraNapomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    korisnikId = table.Column<int>(type: "int", nullable: false),
                    akGodinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisAkGodina", x => x.id);
                    table.ForeignKey(
                        name: "FK_UpisAkGodina_AkademskaGodina_akGodinaId",
                        column: x => x.akGodinaId,
                        principalTable: "AkademskaGodina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UpisAkGodina_KorisnickiNalog_korisnikId",
                        column: x => x.korisnikId,
                        principalTable: "KorisnickiNalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UpisAkGodina_Student_studentId",
                        column: x => x.studentId,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpisAkGodina_akGodinaId",
                table: "UpisAkGodina",
                column: "akGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisAkGodina_korisnikId",
                table: "UpisAkGodina",
                column: "korisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_UpisAkGodina_studentId",
                table: "UpisAkGodina",
                column: "studentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpisAkGodina");
        }
    }
}
