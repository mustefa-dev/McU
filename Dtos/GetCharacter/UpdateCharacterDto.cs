using McU.Models;

namespace McU.Dtos.GetCharacter;

public class UpdateCharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "Mu";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public McUclass Class { get; set; } = McUclass.Marvel;
}