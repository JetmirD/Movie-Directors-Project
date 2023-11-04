using AutoMapper;
using MovieDetyra.Models.DTO;
using MovieDetyra.Models.Entities;

namespace MovieDetyra.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<Director, DirectorDTO>();
        }
    }
}
