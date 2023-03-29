using AutoMapper;
using McU.Dtos;
using McU.Dtos.GetCharacter;
using McU.Models;

namespace McU.Services.CharacterService{
    public class CharacterService : ICharacterService{
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context) {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetCharacterDto>> GetAllCharacters() {
            var dbCharacters = await _context.Characters.ToListAsync();
            return _mapper.Map<List<GetCharacterDto>>(dbCharacters);
        }

        public async Task<GetCharacterDto> GetCharacterById(int id) {
            var dbCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<GetCharacterDto>(dbCharacter);
        }


        public async Task<string?> AddCharacters(AddCharacterDto newCharacter) {
            var dCharacter = _mapper.Map<Character>(newCharacter);
            await _context.Characters.AddAsync(dCharacter);
            await _context.SaveChangesAsync();
            return "Character added successfully.";
        }

        public async Task<string?> UpdateCharacter(UpdateCharacterDto updateCharacterDto) {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == updateCharacterDto.Id);
            if (character == null) {
                return "Character not found.";
            }

            _mapper.Map(updateCharacterDto, character);
            await _context.SaveChangesAsync();
            return "Character updated successfully.";
        }

        public async Task<string> DeleteCharacter(int id) {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            if (character == null) {
                return "Character not found.";
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return "Character deleted successfully.";
        }

        public async Task<AddSkillDto> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill) {
            try {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId);

                if (character is null) {
                    return null!;
                }

                var skill = await _context.Skills
                    .FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill is null) {
                    return null!;
                }

                character.Skills!.Add(skill);
                await _context.SaveChangesAsync();
                return _mapper.Map<AddSkillDto>(character);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}