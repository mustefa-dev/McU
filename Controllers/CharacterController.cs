using McU.Dtos;
using McU.Dtos.GetCharacter;
using McU.Dtos.Skill;
using McU.Models;
using McU.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace McU.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase {
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService) {
        _characterService = characterService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get() {
        return Ok(await _characterService.GetAllCharacters());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingleCharacter(int id) {
        return Ok(await _characterService.GetCharacterById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter) {
        return Ok(await _characterService.AddCharacters(newCharacter));
    }

    [HttpPut("update")]
    public async Task<(string? data, bool success, string? message)> UpdateCharacter(
        UpdateCharacterDto updateCharacter) {
        
      Ok(await _characterService.UpdateCharacter(updateCharacter));

      string? data = null;
      bool success = false;
      return (data, success, null);
    }

    [HttpDelete("{id}")]
    public async Task<(string? data, bool success, string? message)> DeleteCharacter(int id) {
        bool success = false;
        string? data = null;
        Ok(await _characterService.DeleteCharacter(id));
        
        return (data, success, null);
    }

    [HttpPost]
    [Authorize]
    [Route("AddCharacterSkill")]
    public async Task<ActionResult<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
        var characterSkill = await _characterService.AddCharacterSkill(newCharacterSkill);
        
        if (!characterSkill.success)
        {
            return BadRequest(characterSkill.message);
        }
        
        return Ok(characterSkill.data);
    }
    
    }




