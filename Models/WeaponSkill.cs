namespace McU.Models;

public class WeaponSkill
{        public int Id { get; set; }
    public Weapon Weapon { get; set; }
    public int SkillId { get; set; }
    public Skill Skill { get; set; }
}