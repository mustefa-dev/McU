using System.ComponentModel.DataAnnotations.Schema;

namespace McU.Models;

public class Character{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Intelligence { get; set; }
    
    public List<Skill> Skills { get; set; } = null!;
    public Weapon Weapon { get; set; } = null!;

    public bool IsAlive
    {
        get { return HitPoints > 0; }
    }

    public int Victories { get; set; }
    public int Defeats { get; set; }
    public int Fights { get; set; }
}