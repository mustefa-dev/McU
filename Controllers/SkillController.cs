using Microsoft.AspNetCore.Mvc;
using McU.Dtos;
using McU.Services;

namespace McU.Controllers
{
    [ApiController]
    [Route("AddSkill/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddSkill([FromBody] AddSkillDto newSkill)
        {
            var result = await _skillService.AddSkill(newSkill);
            return Ok(result);
        }

    }
}
