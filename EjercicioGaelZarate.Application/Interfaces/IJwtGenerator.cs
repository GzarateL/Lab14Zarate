using EjercicioGaelZarate.Domain.Entities;

namespace EjercicioGaelZarate.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string GenerateToken(User user);
    }
}