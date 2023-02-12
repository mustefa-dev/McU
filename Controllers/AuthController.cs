using McU.Dtos.User;
using McU.Models;
using Microsoft.AspNetCore.Mvc;

namespace McU.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepo)
    {
        _authRepo = authRepo;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
    {
        var response = await _authRepo.Register(
            new User { Username = request.Username }, request.Password
        );
        if(!response.success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
    {
        var response = await _authRepo.Login(request.Username, request.Password);
        if(!response.success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}