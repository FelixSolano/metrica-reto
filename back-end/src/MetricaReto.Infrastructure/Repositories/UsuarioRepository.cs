using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MetricaReto.Domain;
using MetricaReto.Domain.Interfaces;
using MetricaReto.Infrastructure.Persistence;

namespace MetricaReto.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }
}
