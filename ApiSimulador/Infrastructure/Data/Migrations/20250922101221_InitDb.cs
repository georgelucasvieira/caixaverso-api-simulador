using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiSimulador.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    CO_PRODUTO = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NO_PRODUTO = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PC_TAXA_JUROS_ANUAL = table.Column<decimal>(type: "TEXT", nullable: false),
                    PRAZO_MAXIMO_MESES = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.CO_PRODUTO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUTO");
        }
    }
}
