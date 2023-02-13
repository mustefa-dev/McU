using McU.Models;
namespace McU.Dtos;

public  class AddCharacterDto
{
    public AddCharacterDto(bool generateRandomAttribute) {
        GenerateRandomAttribute = generateRandomAttribute;
    }

    public string Name { get; set; } = null!;
    public int HitPoints { get; set; } 
        public int Strength { get; set; } 
        public int Defense { get; set; } 
        public int Intelligence { get; set; } 
        public McUclass Class { get; set; } = McUclass.Marvel;
        public bool GenerateRandomAttribute { get; set; } = false;
    

}