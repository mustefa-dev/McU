namespace McU.Dtos.Fight;

public class AttackResultDto
{
    public string Attacker { get; set; } = null!;
    public string Opponent { get; set; } = null!;
    public int AttackerHp { get; set; }
    public int OpponentHp { get; set; }
    public int Damage { get; set; }
    public string Winner { get; set; } = null!;
    public string Message { get; set; }
}