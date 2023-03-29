namespace McU.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Damage { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; } = null!;
    }
}