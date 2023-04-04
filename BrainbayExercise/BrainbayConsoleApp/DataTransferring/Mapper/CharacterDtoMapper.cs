using AutoMapper;
using BrainbayConsoleApp.DomainModels;
using BrainbayConsoleApp.DomainModels.Entities;

namespace BrainbayConsoleApp.DataTransferring.Mapper
{
    public class CharacterDtoMapper : Profile
    {
        public CharacterDtoMapper()
        {
            CreateMap<Character, CharacterDto>().ReverseMap().ForMember(x => x.Episodes, y =>
            {
                y.MapFrom(z => z.Episodes);
                y.Ignore();
            });
            CreateMap<OriginDto, Origin>().ReverseMap();
            CreateMap<LocationDto, Location>().ReverseMap();
        }
    }
}
