using McU.Models;

namespace McU.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Teleportation", Damage = 30 },
                new Skill { Id = 2, Name = "Jarvis", Damage = 20 },
                new Skill { Id = 3, Name = "Shield", Damage = 50 }
            );
        }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> User => Set<User>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Skill> Skills => Set<Skill>();
    }
}