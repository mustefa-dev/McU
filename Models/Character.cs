using System.ComponentModel.DataAnnotations.Schema;

namespace McU.Models
{
    public class Character{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        
        public List<Skill> Skills { get; set; }
        public Weapon Weapon { get; set; }
        public bool IsAlive
        {
            get { return HitPoints > 0; }
        }
    }
}
