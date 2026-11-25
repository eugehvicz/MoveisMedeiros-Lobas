using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoveisMedeiros.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterCOlunas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inspecoes_Obras_ObraId",
                table: "Inspecoes");

            migrationBuilder.RenameColumn(
                name: "ObraId",
                table: "Inspecoes",
                newName: "ItemObraId");

            migrationBuilder.RenameIndex(
                name: "IX_Inspecoes_ObraId",
                table: "Inspecoes",
                newName: "IX_Inspecoes_ItemObraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inspecoes_ItensObra_ItemObraId",
                table: "Inspecoes",
                column: "ItemObraId",
                principalTable: "ItensObra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inspecoes_ItensObra_ItemObraId",
                table: "Inspecoes");

            migrationBuilder.RenameColumn(
                name: "ItemObraId",
                table: "Inspecoes",
                newName: "ObraId");

            migrationBuilder.RenameIndex(
                name: "IX_Inspecoes_ItemObraId",
                table: "Inspecoes",
                newName: "IX_Inspecoes_ObraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inspecoes_Obras_ObraId",
                table: "Inspecoes",
                column: "ObraId",
                principalTable: "Obras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
