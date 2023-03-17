using AutoMapper;
using McU.Dtos.Weapon;
using McU.Models;
using McU.WeaponService;

namespace McU.Services.WeaponService;

public class WeaponService : IWeaponService{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public WeaponService(DataContext context, IMapper mapper) {
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
}