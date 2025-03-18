using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tarifa = table.Column<double>(type: "float", nullable: false),
                    Ocupantes = table.Column<int>(type: "int", nullable: false),
                    MetrosCuadrados = table.Column<double>(type: "float", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amenidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle Villa", new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9763), new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9700), "", 50.0, "Villa Limache", 5, 200.0 },
                    { 2, "Amenidad 2", "Detalle Villa", new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9772), new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9769), "", 200.0, "Villa Quilpue", 6, 200.0 },
                    { 3, "Amenidad 3", "Detalle Villa", new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9781), new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9777), "", 300.0, "Villa VillaAlemana", 8, 300.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
