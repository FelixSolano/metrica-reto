using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricaReto.Application.DTOs;
using MetricaReto.Application.Interfaces;
using MetricaReto.Domain;
using MetricaReto.Domain.Interfaces;

namespace MetricaReto.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _repository;

    public PedidoService(IPedidoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PedidoDto>> GetAllAsync()
    {
        var pedidos = await _repository.GetAllAsync();
        return pedidos.Select(MapToDto);
    }

    public async Task<PedidoDto?> GetByIdAsync(int id)
    {
        var pedido = await _repository.GetByIdAsync(id);
        return pedido == null ? null : MapToDto(pedido);
    }

    public async Task<PedidoDto> CreateAsync(CreatePedidoDto dto)
    {
        // Business Rules
        if (dto.Total <= 0)
            throw new ArgumentException("El total debe ser mayor a 0.");
            
        if (await _repository.ExistsAsync(dto.NumeroPedido))
            throw new ArgumentException("El número de pedido ya existe.");

        var pedido = new Pedido
        {
            NumeroPedido = dto.NumeroPedido,
            Cliente = dto.Cliente,
            Total = dto.Total,
            Fecha = DateTime.UtcNow,
            Estado = "Registrado"
        };

        var created = await _repository.AddAsync(pedido);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(int id, UpdatePedidoDto dto)
    {
        var pedido = await _repository.GetByIdAsync(id);
        if (pedido == null) return false;
        
        if (dto.Total <= 0)
            throw new ArgumentException("El total debe ser mayor a 0.");

        pedido.Cliente = dto.Cliente;
        pedido.Total = dto.Total;
        pedido.Estado = dto.Estado;

        await _repository.UpdateAsync(pedido);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pedido = await _repository.GetByIdAsync(id);
        if (pedido == null) return false;

        await _repository.DeleteAsync(pedido);
        return true;
    }

    private static PedidoDto MapToDto(Pedido p) => new PedidoDto
    {
        Id = p.Id,
        NumeroPedido = p.NumeroPedido,
        Cliente = p.Cliente,
        Fecha = p.Fecha,
        Total = p.Total,
        Estado = p.Estado
    };
}
