using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using McU.Dtos.Fight;
using McU.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;


namespace McU.Services.FightService{
    public class FightService : IFightService{
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper) {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Dictionary<string, object>> Fight(FightRequestDto request) {
            var result = new Dictionary<string, object>();
            var log = new List<string>();

            try {
                var characters = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .Where(c => request.CharacterIds.Contains(c.Id))
                    .ToListAsync();

                bool defeated = false;
                while (!defeated) {
                    foreach (var attacker in characters) {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed;

                        bool useWeapon = new Random().Next(2) == 0;
                        if (useWeapon) {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeaponAttack(attacker, opponent);
                        }
                        else if (!useWeapon) {
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                            attackUsed = skill.Name;
                            damage = DoSkillAttack(attacker, opponent, skill);
                        }
                        else {
                            log.Add($"{attacker.Name} wasn't able to attack!");
                            continue;
                        }

                        log.Add(
                            $"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage");

                        if (opponent.HitPoints <= 0) {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            log.Add($"{opponent.Name} has been defeated!");
                            log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
                            break;
                        }
                    }
                }

                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });

                await _context.SaveChangesAsync();

                result.Add("log", log);
                result.Add("success", true);
                result.Add("message", "");
            }
            catch (Exception ex) {
                result.Add("log", log);
                result.Add("success", false);
                result.Add("message", ex.Message);
            }

            return result;
        }


        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request) {
            var response = new ServiceResponse<AttackResultDto>();
            try {
                var attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                if (attacker is null || opponent is null || attacker.Skills is null)
                    throw new Exception("Something fishy is going on here...");

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if (skill is null) {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesn't know that skill!";
                    return response;
                }

                int damage = DoSkillAttack(attacker, opponent, skill);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHp = attacker.HitPoints,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        


        private static int DoSkillAttack(Character attacker, Character opponent, Skill skill) {
            int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defeats);

            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request) {
            var response = new ServiceResponse<AttackResultDto>();
            try {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                if (attacker is null || opponent is null || attacker.Weapon is null)
                    throw new Exception("Something fishy is going on here...");
                int damage = DoWeaponAttack(attacker, opponent);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHp = attacker.HitPoints,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        private static int DoWeaponAttack(Character attacker, Character opponent) {
            if (attacker.Weapon is null)
                throw new Exception("Attacker has no weapon!");

            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(opponent.Defeats);

            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<List<HighscoreDto>>> GetHighscore() {
            var characters = await _context.Characters
                .Where(c => c.Fights > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats)
                .ToListAsync();

            var response = new ServiceResponse<List<HighscoreDto>>()
            {
                Data = characters.Select(c => _mapper.Map<HighscoreDto>(c)).ToList()
            };

            return response;
        }
    }
}