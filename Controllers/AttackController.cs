// using McU.Dtos.Fight;
// using McU.Services.WeaponService.Attack;
// using Microsoft.AspNetCore.Mvc;
//
// namespace McU.Controllers;
//
// public class AttackController : ControllerBase{
//     private readonly IAttackService _attackService;
//
//     public AttackController(IAttackService attackService) {
//         _attackService = attackService;
//     }
//
//     [HttpPost("Addweapon")]
//     public async Task<IActionResult> WeaponAttack([FromBody] WeaponAttackDto request) {
//         var (message, success, data) = await _attackService.WeaponAttack(request);
//
//         if (!success)
//             return BadRequest(new { message });
//
//         return Ok(new { data });
//     }
// }