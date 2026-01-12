using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MetricaReto.Domain;
using MetricaReto.Domain.Interfaces;
using MetricaReto.Infrastructure.Persistence;

namespace MetricaReto.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly ApplicationDbContext _context;

    public PedidoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .Where(p => p.IsActive)
            .ToListAsync();
    }

    public async Task<Pedido?> GetByIdAsync(int id)
    {
        return await _context.Pedidos.FindAsync(id);
    }

    public async Task<Pedido> AddAsync(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
        return pedido;
    }

    public async Task UpdateAsync(Pedido pedido)
    {
        _context.Entry(pedido).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Pedido pedido)
    {
        pedido.IsActive = false; // Logical Delete: False = Deleted
        _context.Entry(pedido).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string numeroPedido)
    {
       return await _context.Pedidos.AnyAsync(p => p.NumeroPedido == numeroPedido);
    }
}
