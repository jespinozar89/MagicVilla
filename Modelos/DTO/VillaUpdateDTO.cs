using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Modelos.DTO
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
        [Required]
        public double Tarifa { get; set; }
        [Required]
        public int Ocupantes { get; set; }
        public double MetrosCuadrados { get; set; }
        [Required]
        public string ImagenUrl { get; set; } = string.Empty;
        public string Amenidad { get; set; }= string.Empty;
    }
}