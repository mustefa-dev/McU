using McU.Models;
using McuApi.NET_7.Models;

namespace McuApi.NET_7.Dtos;

public  class AddCharacterDto
{
    public AddCharacterDto(bool generateRandomAttribute) {
        GenerateRandomAttribute = generateRandomAttribute;
    }

    public string Name { get; set; } 
        public int HitPoints { get; set; } 
        public int Strength { get; set; } 
        public int Defense { get; set; } 
        public int Intelligence { get; set; } 
        public McUclass Class { get; set; } = McUclass.Marvel;
        public bool GenerateRandomAttribute { get; set; } = false;
    

}