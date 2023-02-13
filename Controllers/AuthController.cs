using McU.Dtos.User;
using McU.Models;
using Microsoft.AspNetCore.Mvc;

namespace McU.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase{
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepo) {
        _authRepo = authRepo;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserLoginDto userLoginDto) {
        var (data, success, message) =
            await _authRepo.Register(new User { Username = userLoginDto.Username }, userLoginDto.Password);

        if (success) {
            return Ok(data);
        }

        return BadRequest(message);
    }


    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(UserLoginDto request) {
        var response = await _authRepo.Login(request.Username, request.Password);
        if (!response.success) {
            return BadRequest(new { response.message });
        }

        return Ok(response.data);
    }
}