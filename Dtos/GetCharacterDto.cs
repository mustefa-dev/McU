using McU.Models;

namespace McU.Dtos;

public class GetCharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public int HitPoints { get; set; } 
    public int Strength { get; set; } 
    public int Defense { get; set; } 
    public int Intelligence { get; set; } 
    public McUclass Class { get; set; } = McUclass.Marvel;
}