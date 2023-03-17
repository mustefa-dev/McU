using McU.Dtos.Weapon;
using McU.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace McU.Controllers;
[ApiController]
[Route("[controller]")  ]
public class WeaponController : ControllerBase{
    private readonly IWeaponService _weaponService;

    public WeaponController(IWeaponService weaponService){
        _weaponService = weaponService;
    }

    [HttpPost("AddWeapon")]
    public async Task<IActionResult> AddWeapon(AddWeaponDto weapon){
        var (data, success, message) = await _weaponService.AddWeapon(weapon);
        if (success){
            return Ok(new {data, message});
        }
        return BadRequest(new {message});
    }
    
}