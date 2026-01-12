using MetricaReto.Domain;
using BCrypt.Net;

namespace MetricaReto.Infrastructure.Persistence;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Usuarios.Any())
        {
            return;   // DB has been seeded
        }

        var users = new Usuario[]
        {
            new Usuario { Email = "user@email.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), Role = "User" },
            new Usuario { Email = "admin@metrica.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" }
        };

        context.Usuarios.AddRange(users);
        context.SaveChanges();
    }
}
