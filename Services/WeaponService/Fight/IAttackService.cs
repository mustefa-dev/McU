using McU.Dtos.Fight;

namespace McU.Services.WeaponService.Attack;

public interface IAttackService{
    Task<(string? Message, bool Success, AttackResultDto? Data)> WeaponAttack(WeaponAttackDto request);

}