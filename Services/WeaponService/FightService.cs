using AutoMapper;
using McU.Dtos.Fight;
using McU.Dtos.Skill;
using McU.Dtos.Weapon;
using McU.Models;
using McU.Services.CharacterService;

namespace McU.Services.WeaponService;

public class FightService : IFightService{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    


    public FightService(DataContext context, IMapper mapper, ICharacterService characterService) {
        _context = context;
        _mapper = mapper;

    }


    public async Task<(string, bool, string)> AddWeapon(AddWeaponDto weapon) {
        var character = await _context.Characters.FindAsync(weapon.CharacterId);
        if (character == null) {
            return (string.Empty, false, "Character not found");
        }

        var existingWeapon = await _context.Weapons.FirstOrDefaultAsync(w => w.CharacterId == character.Id);
        if (existingWeapon != null) {
            return (string.Empty, false, "Character already has a weapon");
        }

        var weaponModel = _mapper.Map<Weapon>(weapon);
        weaponModel.Character = character;

        _context.Weapons.Add(weaponModel);
        await _context.SaveChangesAsync();

        return (string.Empty, true, "Weapon added successfully");
    }

    


    public async Task<(bool Success, string Message, AttackResultDto Data)> SkillAttack(SkillAttackDto request)
    {
        try
        {
            var attacker = await _context.Characters
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
            var opponent = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

            if (attacker is null || opponent is null || attacker.Skills is null)
                throw new Exception("Something fishy is going on here...");

            var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
            if (skill is null)
            {
                return (false, $"{attacker.Name} doesn't know that skill!", null)!;
            }

            double damageMultiplier = (attacker.Strength + attacker.Intelligence) / (double)opponent.Defense;
            int damage = (int)(skill.Damage * damageMultiplier) + new Random().Next(-10, 10);

            if (opponent.HitPoints <= 0)
                opponent.HitPoints = 0;

            opponent.HitPoints -= damage;

            await _context.SaveChangesAsync();

            var data = new AttackResultDto
            {
                Attacker = attacker.Name,
                Opponent = opponent.Name,
                AttackerHp = attacker.HitPoints,
                OpponentHp = opponent.HitPoints,
                Damage = damage
            };

            if (opponent.HitPoints <= 0)
                return (true, $"{opponent.Name} has been defeated!", data);

            return (true, "", data);
        }
        catch (Exception ex)
        {
            return (false, ex.Message, null)!;
        }
    }
    
}

