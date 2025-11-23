using EjercicioGaelZarate.Domain.Entities;

namespace EjercicioGaelZarate.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}