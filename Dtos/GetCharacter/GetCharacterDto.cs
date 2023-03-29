using McU.Dtos.Skill;
using McU.Dtos.Weapon;
using McU.Models;

namespace McU.Dtos.GetCharacter;

public class GetCharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Intelligence { get; set; }
    public McUclass Class { get; set; }
    public GetWeaponDto? Weapon { get; set; }
    public GetWeaponDto? Damage { get; set; }
    
    public List<GetSkillDto> Skills { get; set; } = null!;
}