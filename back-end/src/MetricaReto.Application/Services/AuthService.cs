using MetricaReto.Application.Interfaces;
using MetricaReto.Domain.Interfaces;

namespace MetricaReto.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(IUsuarioRepository usuarioRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _usuarioRepository = usuarioRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _usuarioRepository.GetByEmailAsync(email);
        if (user == null) return null;

        // Verify password
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
             return null;
             
        return _jwtTokenGenerator.GenerateToken(user);
    }
}
