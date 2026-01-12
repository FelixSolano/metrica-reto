using MetricaReto.Domain;

namespace MetricaReto.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Usuario user);
}
