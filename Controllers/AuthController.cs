using McU.Dtos.User;
using McU.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace McU.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase{
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepo) {
        _authRepo = authRepo;
    }
    [AllowAnonymous]
    [Authorize(Roles = "Admin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userLoginDto) {
        var result = await _authRepo.Register(userLoginDto); 
        if (result.error != null) return BadRequest(result.error);
        return Ok(result.user);
        
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(UserLoginDto request) {
        var response = await _authRepo.Login(request.Username, request.Password);
        if (!response.success) {
            return BadRequest(new { response.message });
        }

        return Ok(response.data);
    }
    
}