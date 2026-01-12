using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetricaReto.Application.Interfaces;

namespace MetricaReto.API.Controllers;

[ApiController]
[Route("auth")] // Requirement: /auth/login
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.LoginAsync(request.Email, request.Password);
        if (token == null)
            return Unauthorized(new { message = "Invalid credentials" });

        return Ok(new { token, expiresIn = 3600 });
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
