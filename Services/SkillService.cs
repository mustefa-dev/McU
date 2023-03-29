using McU.Dtos;
using McU.Models;
using AutoMapper;
using McU.Dtos.Skill;

namespace McU.Services
{
    public class SkillService: ISkillService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SkillService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> AddSkill(AddSkillDto newSkill)
        {
            try
            {
                var skillExists = _context.Skills.Any(s => s.Name == newSkill.Name);
                if (skillExists)
                {
                    return "Error: Skill with the same name already exists";
                }

                var skill = _mapper.Map<Skill>(newSkill);
                await _context.Skills.AddAsync(skill);
                await _context.SaveChangesAsync();
                var getSkill = _mapper.Map<GetSkillDto>(skill);
                return $"Skill {getSkill.Name} added successfully";
            }
            catch (ArgumentException ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}