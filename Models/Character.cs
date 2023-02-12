using System.ComponentModel.DataAnnotations.Schema;

namespace McU.Models;

public class Character
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int HitPoints { get; set; }
    public int Strength { get; set; } 
    public int Defense { get; set; } 
    public int Intelligence { get; set; }
    public McUclass Class { get; set; } = McUclass.Marvel;
    public User? User { get; set; } = null!;
}