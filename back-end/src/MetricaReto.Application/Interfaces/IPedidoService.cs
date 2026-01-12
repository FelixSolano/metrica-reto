using System.Collections.Generic;
using System.Threading.Tasks;
using MetricaReto.Application.DTOs;

namespace MetricaReto.Application.Interfaces;

public interface IPedidoService
{
    Task<IEnumerable<PedidoDto>> GetAllAsync();
    Task<PedidoDto?> GetByIdAsync(int id);
    Task<PedidoDto> CreateAsync(CreatePedidoDto dto);
    Task<bool> UpdateAsync(int id, UpdatePedidoDto dto);
    Task<bool> DeleteAsync(int id);
}
