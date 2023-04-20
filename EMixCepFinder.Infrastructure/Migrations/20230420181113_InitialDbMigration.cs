using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMixCepFinder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CEP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cep = table.Column<string>(type: "char(9)", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Localidade = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UF = table.Column<string>(type: "char(2)", nullable: false),
                    Unidade = table.Column<long>(type: "bigint", nullable: false),
                    Ibge = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<string>(type: "nvarchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEP", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CEP_Cep",
                table: "CEP",
                column: "Cep",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CEP");
        }
    }
}
