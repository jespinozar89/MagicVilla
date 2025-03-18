﻿// <auto-generated />
using System;
using MagicVillaAPI.Modelos.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVillaAPI.Modelos.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MetrosCuadrados")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Detalle Villa",
                            FechaActualizacion = new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9763),
                            FechaCreacion = new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9700),
                            ImagenUrl = "",
                            MetrosCuadrados = 50.0,
                            Nombre = "Villa Limache",
                            Ocupantes = 5,
                            Tarifa = 200.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "Amenidad 2",
                            Detalle = "Detalle Villa",
                            FechaActualizacion = new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9772),
                            FechaCreacion = new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9769),
                            ImagenUrl = "",
                            MetrosCuadrados = 200.0,
                            Nombre = "Villa Quilpue",
                            Ocupantes = 6,
                            Tarifa = 200.0
                        },
                        new
                        {
                            Id = 3,
                            Amenidad = "Amenidad 3",
                            Detalle = "Detalle Villa",
                            FechaActualizacion = new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9781),
                            FechaCreacion = new DateTime(2025, 3, 17, 12, 16, 39, 101, DateTimeKind.Local).AddTicks(9777),
                            ImagenUrl = "",
                            MetrosCuadrados = 300.0,
                            Nombre = "Villa VillaAlemana",
                            Ocupantes = 8,
                            Tarifa = 300.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
