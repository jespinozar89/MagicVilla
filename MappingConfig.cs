using AutoMapper;
using MagicVillaAPI.Modelos;
using MagicVillaAPI.Modelos.DTO;

namespace MagicVillaAPI
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa,VillaDTO>();
            CreateMap<VillaDTO,Villa>();

            CreateMap<Villa,VillaCreateDTO>().ReverseMap();
            CreateMap<Villa,VillaUpdateDTO>().ReverseMap();

            CreateMap<NumeroVilla,NumeroVillaDTO>().ReverseMap();
            CreateMap<NumeroVilla,NumeroVillaCreateDTO>().ReverseMap();
            CreateMap<NumeroVilla,NumeroVillaUpdateDTO>().ReverseMap();
            
        }
    }
}