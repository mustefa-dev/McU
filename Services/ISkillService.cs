using McU.Dtos;
namespace McU.Services
{
    public interface ISkillService
    {
        Task<string> AddSkill(AddSkillDto newSkill);
    }
}