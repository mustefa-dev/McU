using AutoMapper;
using McU.Dtos;
using McU.Models;

namespace McU.Services.CharacterService{
    public class CharacterService : ICharacterService{
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor) {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters() {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(dbCharacters);
            return serviceResponse;
        }


        public async Task<ServiceResponse<GetCharacterDto?>> GetCharacterById(int id) {
            var serviceResponse = new ServiceResponse<GetCharacterDto?>();
            var dCharacter = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            if (dCharacter == null) {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Character with id {id} not found.";
                return serviceResponse;
            }

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dCharacter);
            return serviceResponse;
        }

        public async Task<(string? data, bool success, string? message)> AddCharacters(AddCharacterDto newCharacter) {
            var dCharacter = _mapper.Map<Character>(newCharacter);
            await _context.Characters.AddAsync(dCharacter);
            await _context.SaveChangesAsync(); 
            _mapper.Map<List<GetCharacterDto>>(_context.Characters);
            bool success = false;
            return (null, success, null);
        }


        public async Task<(string? data, bool success, string? message)> UpdateCharacter(
            UpdateCharacterDto updateCharacterDto) {
            var success = false;
            var message = "Character not found";
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == updateCharacterDto.Id);
            if (character == null) {
                success = false;
                message = $"Character with id {updateCharacterDto.Id} not found.";
                return (null, success, message);
            }

            _mapper.Map(updateCharacterDto, character);
            await _context.SaveChangesAsync();
            return (null, success, message);
        }


        public async Task<(string? data, bool success, string? message)> DeleteCharacter(int id) {
            var success = false;
            var message = "Character not found";
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            if (character == null) {
                return (null, success, message);
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return (null, success, message);
        }
    }
}