using AutoMapper;
using McU.Dtos;
using McU.Models;

namespace McU;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
        CreateMap<UpdateCharacterDto, Character>();
        CreateMap<Character, GetCharacterDto>();
    }
}