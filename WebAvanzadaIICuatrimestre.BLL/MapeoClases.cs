using AutoMapper;

namespace WebAvanzadaIICuatrimestre.BLL
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            // Map PartidoPolitico entity to PartidoPoliticoDto and map the navigation property FkrepresentanteLegalNavigation -> RepresentanteLegal
            CreateMap<DAL.Entidades.PartidoPolitico, Dtos.PartidoPoliticoDto>()
                .ForMember(dest => dest.RepresentanteLegal, opt => opt.MapFrom(src => src.FkrepresentanteLegalNavigation))
                .ReverseMap()
                .ForMember(dest => dest.FkrepresentanteLegal, opt => opt.MapFrom(src => src.RepresentanteLegal != null ? src.RepresentanteLegal.Id : src.FkrepresentanteLegal));
            CreateMap<DAL.Entidades.RepresentanteLegal, Dtos.RepresentanteLegalDto>().ReverseMap();
            CreateMap<DAL.Entidades.Telefono, Dtos.TelefonoDto>().ReverseMap();
            CreateMap<DAL.Entidades.Correo, Dtos.CorreoDto>().ReverseMap();
            CreateMap<DAL.Entidades.Votante, Dtos.VotanteDto>().ReverseMap();
        }
    }

    //MAPPER
}
