using MagicVillaAPI.Modelos.DTO;

namespace MagicVillaAPI.Datos
{
    public class VillaStore
    {
        public static List<VillaDTO> VillasList = new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Nombre = "Villa 1",Ocupantes = 4 ,MetrosCuadrados = 100},
            new VillaDTO { Id = 2, Nombre = "Villa 2",Ocupantes = 4 ,MetrosCuadrados = 100},
            new VillaDTO { Id = 3, Nombre = "Villa 3",Ocupantes = 4 ,MetrosCuadrados = 100},
        };  
    }
}