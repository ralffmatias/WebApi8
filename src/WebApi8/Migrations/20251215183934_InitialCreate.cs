using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi8.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "autores",
                columns: table => new
                {
                    id_autor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_autor = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_autores", x => x.id_autor);
                });

            migrationBuilder.CreateTable(
                name: "livros",
                columns: table => new
                {
                    id_livro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_titulo = table.Column<string>(type: "varchar(150)", nullable: false),
                    id_autor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_livros", x => x.id_livro);
                    table.ForeignKey(
                        name: "FK_livros_autores_id_autor",
                        column: x => x.id_autor,
                        principalTable: "autores",
                        principalColumn: "id_autor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_livros_id_autor",
                table: "livros",
                column: "id_autor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "livros");

            migrationBuilder.DropTable(
                name: "autores");
        }
    }
}
