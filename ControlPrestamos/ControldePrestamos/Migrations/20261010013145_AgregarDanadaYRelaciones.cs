using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControldePrestamos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDanadaYRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Danada",
                table: "Herramientas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_EstudianteId",
                table: "Prestamos",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_HerramientaId",
                table: "Prestamos",
                column: "HerramientaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Estudiantes_EstudianteId",
                table: "Prestamos",
                column: "EstudianteId",
                principalTable: "Estudiantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Herramientas_HerramientaId",
                table: "Prestamos",
                column: "HerramientaId",
                principalTable: "Herramientas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Estudiantes_EstudianteId",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Herramientas_HerramientaId",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_EstudianteId",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_HerramientaId",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "Danada",
                table: "Herramientas");
        }
    }
}
