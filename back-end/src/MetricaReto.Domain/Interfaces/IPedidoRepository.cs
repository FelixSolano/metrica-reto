using System.Collections.Generic;
using System.Threading.Tasks;
using MetricaReto.Domain;

namespace MetricaReto.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<IEnumerable<Pedido>> GetAllAsync();
    Task<Pedido?> GetByIdAsync(int id);
    Task<Pedido> AddAsync(Pedido pedido);
    Task UpdateAsync(Pedido pedido);
    Task DeleteAsync(Pedido pedido);
    Task<bool> ExistsAsync(string numeroPedido);
}
