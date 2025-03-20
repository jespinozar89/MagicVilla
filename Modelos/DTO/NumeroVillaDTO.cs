using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.Modelos.DTO
{
    public class NumeroVillaDTO
    {
        [Required]
        public int VillaNO { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string DetalleEspecial { get; set; } = string.Empty;
    }
}