using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IpbShare.Infrastructure.Migrations
{
    public partial class ipbshare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCategoria = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escolas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeEscola = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomePais = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCurso = table.Column<string>(maxLength: 256, nullable: false),
                    NumAlunos = table.Column<int>(nullable: false),
                    EscolaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeEquipamento = table.Column<string>(maxLength: 256, nullable: false),
                    DescricaoEquipamento = table.Column<string>(nullable: true),
                    IsReservado = table.Column<bool>(nullable: false),
                    Thumb = table.Column<byte[]>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: false),
                    EscolaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Escolas_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escolas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    IsAdministrator = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    PaisId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilizadores_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Utilizadores_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    EquipamentoId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    DataReserva = table.Column<DateTime>(nullable: false),
                    DataEntrega = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => new { x.EquipamentoId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Reservas_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_Utilizadores_UserId",
                        column: x => x.UserId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Escolas",
                columns: new[] { "Id", "NomeEscola" },
                values: new object[] { 1, "ESTIG" });

            migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "Id", "NomePais" },
                values: new object[] { 1, "Portugal" });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "EscolaId", "NomeCurso", "NumAlunos" },
                values: new object[] { 1, 1, "EI", 0 });

            migrationBuilder.InsertData(
                table: "Utilizadores",
                columns: new[] { "Id", "CursoId", "Email", "IsAdministrator", "Nome", "PaisId", "Password" },
                values: new object[] { 1, 1, "admin@ipb.pt", true, "admin", 1, "D033E22AE348AEB5660FC2140AEC35850C4DA997" });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_NomeCategoria",
                table: "Categorias",
                column: "NomeCategoria",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_EscolaId",
                table: "Cursos",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_NomeCurso",
                table: "Cursos",
                column: "NomeCurso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_CategoriaId",
                table: "Equipamentos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_EscolaId",
                table: "Equipamentos",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_NomeEquipamento",
                table: "Equipamentos",
                column: "NomeEquipamento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Escolas_NomeEscola",
                table: "Escolas",
                column: "NomeEscola",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paises_NomePais",
                table: "Paises",
                column: "NomePais",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UserId",
                table: "Reservas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_CursoId",
                table: "Utilizadores",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_Email",
                table: "Utilizadores",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_PaisId",
                table: "Utilizadores",
                column: "PaisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "Escolas");
        }
    }
}
