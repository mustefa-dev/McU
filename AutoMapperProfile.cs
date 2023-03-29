using AutoMapper;
using McU.Dtos;
using McU.Dtos.Fight;
using McU.Dtos.GetCharacter;
using McU.Dtos.Skill;
using McU.Dtos.Weapon;
using McU.Models;

namespace McU
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<AddWeaponDto, Weapon>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Character, HighscoreDto>();
            CreateMap<Character, AddSkillDto>();
            CreateMap<AddSkillDto, Skill>();
        }
    }
}