using Microsoft.EntityFrameworkCore;
using MetricaReto.Domain;

namespace MetricaReto.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("pedidos");
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.NumeroPedido).HasColumnName("numeropedido");
            entity.Property(p => p.Cliente).HasColumnName("cliente");
            entity.Property(p => p.Fecha).HasColumnName("fecha");
            entity.Property(p => p.Estado).HasColumnName("estado");
            entity.Property(p => p.IsActive).HasColumnName("isactive");

            entity.Property(p => p.Total)
                .HasColumnName("total")
                .HasPrecision(10, 2);

            entity.HasIndex(p => p.NumeroPedido).IsUnique();
        });
    }
}
