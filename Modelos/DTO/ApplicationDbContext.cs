using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Modelos.DTO
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Nombre = "Villa Limache",
                    Detalle = "Detalle Villa",
                    Tarifa = 200,
                    Ocupantes = 5,
                    MetrosCuadrados = 50,
                    ImagenUrl = "",
                    Amenidad = "amenidad 1",
                    FechaCreacion = System.DateTime.Now,
                    FechaActualizacion = System.DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Nombre = "Villa Quilpue",
                    Detalle = "Detalle Villa",
                    Tarifa = 200,
                    Ocupantes = 6,
                    MetrosCuadrados = 200,
                    ImagenUrl = "",
                    Amenidad = "Amenidad 2",
                    FechaCreacion = System.DateTime.Now,
                    FechaActualizacion = System.DateTime.Now
                },
                new Villa
                {
                    Id = 3,
                    Nombre = "Villa VillaAlemana",
                    Detalle = "Detalle Villa",
                    Tarifa = 300,
                    Ocupantes = 8,
                    MetrosCuadrados = 300,
                    ImagenUrl = "",
                    Amenidad = "Amenidad 3",
                    FechaCreacion = System.DateTime.Now,
                    FechaActualizacion = System.DateTime.Now
                }
            );
        }
    }
}