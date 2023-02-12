using McU.Dtos;

namespace McU.Services.CharacterService;

public interface ICharacterService{
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
    Task<ServiceResponse<GetCharacterDto?>> GetCharacterById(int id);
    Task<(string? data, bool success, string? message)> AddCharacters(AddCharacterDto newCharacter);
    Task<(string? data, bool success, string? message)> UpdateCharacter(UpdateCharacterDto updateCharacterDto);
    Task<(string? data, bool success, string? message)> DeleteCharacter(int id);
}