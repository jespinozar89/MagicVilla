using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MagicVillaAPI.Modelos
{
    public class NumeroVilla
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNO { get; set; }
        [Required]
        public int VillaId { get; set; }
        [ForeignKey("VillaId")]
        public  Villa Villa { get; set; }
        public string DetalleEspecial { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}