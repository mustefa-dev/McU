using System.Text.Json.Serialization;

namespace McU.Dtos.GetCharacter;


public class AddCharacterSkillDto
{
    public int CharacterId { get; set; }
    public int SkillId { get; set; }
}
