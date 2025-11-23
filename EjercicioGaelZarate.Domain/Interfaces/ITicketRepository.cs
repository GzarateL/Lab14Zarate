using EjercicioGaelZarate.Domain.Entities;

namespace EjercicioGaelZarate.Domain.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(Guid userId); // Corregido a Guid
    }
}