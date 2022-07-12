using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AlterPedidoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorias_Pedidos_PedidoId",
                table: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Categorias_PedidoId",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Categorias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PedidoId",
                table: "Categorias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_PedidoId",
                table: "Categorias",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorias_Pedidos_PedidoId",
                table: "Categorias",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
