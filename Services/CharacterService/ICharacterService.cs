using McU.Dtos;
using McU.Dtos.GetCharacter;
using McU.Dtos.Skill;
using McU.Models;

namespace McU.Services.CharacterService;

public interface ICharacterService{
    Task<List<GetCharacterDto>> GetAllCharacters();
    Task<GetCharacterDto> GetCharacterById(int id);
    Task<string?> AddCharacters(AddCharacterDto newCharacter);
    Task<string?> UpdateCharacter(UpdateCharacterDto updateCharacterDto);
    Task<string> DeleteCharacter(int id);
    Task<AddSkillDto> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);

}