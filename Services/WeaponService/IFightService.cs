using dotnet_rpg.Dtos.Fight;
using McU.Dtos.Fight;
using McU.Dtos.Skill;
using McU.Dtos.Weapon;

namespace McU.Services.WeaponService;

public interface IFightService{
    Task<(string, bool, string)> AddWeapon(AddWeaponDto weapon);
    Task <(bool Success, string Message, AttackResultDto Data)> SkillAttack(SkillAttackDto request);


}