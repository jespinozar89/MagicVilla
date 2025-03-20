using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNumeroVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    VillaNO = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.VillaNO);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amenidad", "FechaActualizacion", "FechaCreacion" },
                values: new object[] { "amenidad 1", new DateTime(2025, 3, 20, 11, 14, 37, 40, DateTimeKind.Local).AddTicks(3759), new DateTime(2025, 3, 20, 11, 14, 37, 40, DateTimeKind.Local).AddTicks(3714) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 3, 20, 11, 14, 37, 40, DateTimeKind.Local).AddTicks(3768), new DateTime(2025, 3, 20, 11, 14, 37, 40, DateTimeKind.Local).AddTicks(3765) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 3, 20, 11, 14, 37, 40, DateTimeKind.Local).AddTicks(3775), new DateTime(2025, 3, 20, 11, 14, 37, 40, DateTimeKind.Local).AddTicks(3772) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_VillaId",
                table: "NumeroVillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amenidad", "FechaActualizacion", "FechaCreacion" },
                values: new object[] { "", new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9763), new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9772), new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9769) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9781), new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9777) });
        }
    }
}
