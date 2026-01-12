using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetricaReto.Domain;

public class Pedido
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string NumeroPedido { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Cliente { get; set; } = string.Empty;

    public DateTime Fecha { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Total { get; set; }

    [Required]
    [MaxLength(50)]
    public string Estado { get; set; } = "Registrado";

    // Opcional: Soft delete
    // Soft delete: True = Visible/Active, False = Deleted at user request
    public bool IsActive { get; set; } = true;
}
