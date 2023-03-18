using McU.Dtos.Fight;
using McU.Services.WeaponService.Attack;

public class AttackService : IAttackService
{
    private readonly DataContext _context;
    
    public AttackService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<(string? Message, bool Success, AttackResultDto Data)> WeaponAttack(WeaponAttackDto request)
    {
        var response = new ServiceResponse<AttackResultDto>();
        bool rematchRequired = false;

        try
        {
            var attacker = await _context.Characters
                .Include(c => c.Weapon)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
            var opponent = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

            if (attacker is null || opponent is null || attacker.Weapon is null)
                throw new Exception("Something fishy is going on here...");

            var attackPower = 0.4 * attacker.Strength + 0.3 * attacker.Intelligence + 0.2 * attacker.HitPoints + 0.1 * attacker.Defense;

            int damage = (int)Math.Round(attackPower * (1 - (double)opponent.Defense / 100));

            if (damage > opponent.HitPoints)
                damage = opponent.HitPoints;

            opponent.HitPoints -= damage;

            if (opponent.HitPoints <= 0)
            {
                response.Message = $"{opponent.Name} has been defeated!";
                rematchRequired = true;
            }

            await _context.SaveChangesAsync();

            var resultDto = new AttackResultDto
            {
                Attacker = attacker.Name,
                Opponent = opponent.Name,
                AttackerHp = attacker.HitPoints,
                OpponentHp = opponent.HitPoints,
                Damage = damage
            };

            if (rematchRequired)
            {
                response.Message += " Rematch required.";
                response.Success = false;
            }
            else
            {
                response.Data = resultDto;
                response.Success = true;
            }

            return (response.Message, response.Success, response.Data);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return (response.Message, response.Success, null);
        }
    }
}