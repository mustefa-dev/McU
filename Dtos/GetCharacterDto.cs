using McU.Dtos.Skill;
using McU.Dtos.Weapon;
using McU.Models;

public class GetCharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Intelligence { get; set; }
    public McUclass Class { get; set; }
    public GetWeaponDto? Weapon { get; set; }
    public List<GetSkillDto> Skills { get; set; } = new List<GetSkillDto>();
}