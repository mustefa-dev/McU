using McU.Dtos;
using McU.Dtos.GetCharacter;
using McU.Dtos.Weapon;
using McU.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace McU.Controllers{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase{
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService) {
            _weaponService = weaponService;
        }

        [HttpPost("UpdateWeapon")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateWeapon(UpdateWeaponDto newWeapon) {
            return Ok(await _weaponService.UpdateWeapon(newWeapon));
        }
    }
}