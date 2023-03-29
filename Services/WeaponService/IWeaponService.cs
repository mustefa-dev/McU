using McU.Dtos;
using McU.Dtos.GetCharacter;
using McU.Dtos.Weapon;

namespace McU.Services.WeaponService.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> UpdateWeapon(UpdateWeaponDto updateWeaponDto);
    }
}