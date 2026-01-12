using System.Threading.Tasks;

namespace MetricaReto.Application.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(string email, string password);
}
