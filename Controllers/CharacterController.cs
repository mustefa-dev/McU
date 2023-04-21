using McU.Dtos;
using McU.Dtos.GetCharacter;
using McU.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace McU.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService) {
        _characterService = characterService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get() {
        return Ok(await _characterService.GetAllCharacters());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacterById(int id) {
        return Ok(await _characterService.GetCharacterById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter) {
        return Ok(await _characterService.AddCharacters(newCharacter));
    }

    [HttpPut("update")]
    public async Task<OkObjectResult> UpdateCharacter(UpdateCharacterDto updateCharacter) {
        return Ok(await _characterService.UpdateCharacter(updateCharacter));
    }

    [HttpDelete("{id:int}")]
    public async Task<OkObjectResult> DeleteCharacter(int id) {
        return Ok(await _characterService.DeleteCharacter(id));
    }

    [HttpPost("AddCharacterSkill")]
    public async Task<OkObjectResult> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill) {
        return Ok (await _characterService.AddCharacterSkill(newCharacterSkill));
    }
}