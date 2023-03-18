using McU.Dtos.Fight;
using McU.Dtos.Weapon;
using McU.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace McU.Controllers;

[ApiController]
[Route("[controller]")]
public class WeaponController : ControllerBase{
    private readonly IFightService _fightService;

    public WeaponController(IFightService fightService) {
        _fightService = fightService;
    }

    [HttpPost("AddWeapon")]
    public async Task<IActionResult> AddWeapon(AddWeaponDto weapon) {
        var (data, success, message) = await _fightService.AddWeapon(weapon);
        if (success) {
            return Ok(new { data, message });
        }

        return BadRequest(new { message });
    }

    [HttpPost("SkillAttack")]

    public async Task<IActionResult> SkillAttack(SkillAttackDto request) {
        var (success, message, data) = await _fightService.SkillAttack(request);
        if (success) {
            return Ok(new { data, message });
        }

        return BadRequest(new { message });
    }
    
    
}