using System.Security.Claims;
using AutoMapper;
using McU.Dtos;
using McU.Dtos.GetCharacter;
using McU.Dtos.Weapon;
using McU.Models;

namespace McU.Services.WeaponService.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateWeapon(UpdateWeaponDto updatedWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == updatedWeapon.CharacterId &&
                                              c.Id == int.Parse(_httpContextAccessor.HttpContext!.User
                                                  .FindFirstValue(ClaimTypes.NameIdentifier)!));

                if (character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }

                var weapon = await _context.Weapons
                    .FirstOrDefaultAsync(w => w.Id == updatedWeapon.Id && w.CharacterId == updatedWeapon.CharacterId);

                if (weapon is null)
                {
                    response.Success = false;
                    response.Message = "Weapon not found.";
                    return response;
                }

                weapon.Name = updatedWeapon.Name;
                weapon.Damage = updatedWeapon.Damage;

                _context.Weapons.Update(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}