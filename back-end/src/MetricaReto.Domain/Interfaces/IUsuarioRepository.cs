using System.Threading.Tasks;
using MetricaReto.Domain;

namespace MetricaReto.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByEmailAsync(string email);
    Task AddAsync(Usuario usuario);
}
