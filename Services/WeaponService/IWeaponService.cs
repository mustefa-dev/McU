using McU.Dtos.Weapon;

namespace McU.WeaponService;

public interface IWeaponService{
    Task<(string, bool, string)> AddWeapon(AddWeaponDto weapon);

}