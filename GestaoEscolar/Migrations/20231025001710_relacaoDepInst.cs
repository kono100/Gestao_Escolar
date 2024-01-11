using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestao.Migrations
{
    /// <inheritdoc />
    public partial class relacaoDepInst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "fk_InstituicaoID",
                table: "Departamento",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_fk_InstituicaoID",
                table: "Departamento",
                column: "fk_InstituicaoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departamento_Instituicao_fk_InstituicaoID",
                table: "Departamento",
                column: "fk_InstituicaoID",
                principalTable: "Instituicao",
                principalColumn: "InstituicaoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamento_Instituicao_fk_InstituicaoID",
                table: "Departamento");

            migrationBuilder.DropIndex(
                name: "IX_Departamento_fk_InstituicaoID",
                table: "Departamento");

            migrationBuilder.DropColumn(
                name: "fk_InstituicaoID",
                table: "Departamento");
        }
    }
}
