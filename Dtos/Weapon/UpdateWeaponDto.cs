namespace McU.Dtos.Weapon;

public class UpdateWeaponDto{
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }
    public int CharacterId { get; set; }
    public int Id { get; set; }
    public int User { get; set; }
    
}