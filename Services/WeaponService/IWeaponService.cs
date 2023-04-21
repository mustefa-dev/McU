using McU.Dtos.GetCharacter;
using McU.Dtos.Weapon;

namespace McU.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<GetCharacterDto> UpdateWeapon(UpdateWeaponDto updateWeaponDto);

    }
}