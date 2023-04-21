using System.Security.Claims;
using AutoMapper;
using McU.Dtos.GetCharacter;
using McU.Dtos.Weapon;
using McU.Models;

namespace McU.Services.WeaponService{
    public class WeaponService : IWeaponService{
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper) {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetCharacterDto> UpdateWeapon(UpdateWeaponDto updatedWeapon) {
            try {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == updatedWeapon.CharacterId &&
                                              c.Id == int.Parse(_httpContextAccessor.HttpContext!.User
                                                  .FindFirstValue(ClaimTypes.NameIdentifier)!));

                if (character is null) {
                    throw new Exception("Character not found.");
                }

                var weapon = await _context.Weapons
                    .FirstOrDefaultAsync(w => w.Id == updatedWeapon.Id && w.CharacterId == updatedWeapon.CharacterId);

                if (weapon is null) {
                    throw new Exception("Weapon not found.");
                }

                weapon.Name = updatedWeapon.Name;
                weapon.Damage = updatedWeapon.Damage;

                _context.Weapons.Update(weapon);
                await _context.SaveChangesAsync();

                return _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}