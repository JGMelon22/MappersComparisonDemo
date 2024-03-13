using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MappersWebApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    produto_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    preco = table.Column<float>(type: "REAL", nullable: false),
                    disponivel = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_produto", x => x.produto_id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_produto_id",
                table: "produtos",
                column: "produto_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produtos");
        }
    }
}
